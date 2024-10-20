﻿using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.RapidDomain;
using System;
using System.Windows.Forms;


namespace Photogrametry
{
    class RapidFunctions
    {
        ABB.Robotics.Controllers.Controller objController;
        private NetworkScanner objNetworkWatcher = null;

        public ABB.Robotics.Controllers.RapidDomain.Task[] tasks = null;
        Mastership m;
        
        private bool _run;
        
        double minimumMove = 5;

        public
        RapidData data5;
        RapidData data6;
        RapidData data7;
        RapidData data8;
        RapidData controllerWaiting;
        

        public Form1 fr;
        public gphoto_CTRL gphoto;
        public decimal waittime = 100;
        string s;
        public string IP;
        public Controller controller = null;
        public RapidFunctions(Form1 _form1)
        {
            this.fr = _form1;
            
        }

        //Controller Scanner
        //Scan for and add controllers to the list
        public ControllerInfoCollection ScanControllers()
        {
            try
            {
                // Create A Robo Studio Controller Connector
                this.objNetworkWatcher = new NetworkScanner();
                objNetworkWatcher.Scan();
                ControllerInfoCollection controllersList = objNetworkWatcher.Controllers;
                return controllersList;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
                fr.LogMessage(ex.Message + ex.Source + ex.StackTrace);
                return null;
            }

        }
        
        //Connect to the Robo Studio Controller
        public void ConnectController(ListViewItem CTRLSelect)
        {
            try
            {
                ListViewItem item = CTRLSelect;
                if (item.Tag != null)
                {
                    ControllerInfo controllerInfo = (ControllerInfo)item.Tag;
                    if (controllerInfo.Availability == Availability.Available)
                    {
                        this.controller = ControllerFactory.CreateFrom(controllerInfo);
                    }
                    else
                    {
                        MessageBox.Show("Selected controller not available.");
                    }
                }
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
                fr.LogMessage(ex.Message + ex.Source + ex.StackTrace);
            }
        }

        public void Start()
        {
            try
            {
                if (controller.OperatingMode == ControllerOperatingMode.Auto)
                {
                    tasks = controller.Rapid.GetTasks();
                    tasks[0].ProgramPointerChanged += new EventHandler<ProgramPositionEventArgs>(ProgramPointer_Changed);
                    using (m = Mastership.Request(controller.Rapid))
                    {
                        //Perform operation
                        tasks[0].ResetProgramPointer(); 
                        controller.Rapid.Start();
                    }
                    controllerWaiting = controller.Rapid.GetRapidData("T_ROB1", "Module1", "extern_wait");
                    controllerWaiting.ValueChanged += new EventHandler<DataValueChangedEventArgs>(ControllerWaitCheck);
                    }
                else
                {
                    MessageBox.Show(
                        "Automatic mode is required to start execution from a remote client.");


                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("An Error Has Occurred: " + ex.Message);
                fr.LogMessage(ex.Message + ex.Source + ex.StackTrace);
            }

        }

        
        //Stop Everything
        public void Stop()
        {
            try
            {
                //Stop the Send data while loop
                _run = false;
                
                if (controller != null)
                {
                    if (controller.Rapid.ExecutionStatus ==
                           ABB.Robotics.Controllers.RapidDomain.ExecutionStatus.Running)
                    {

                        try
                        {
                            using (m = Mastership.Request(controller.Rapid))
                            {
                                m.ReleaseOnDispose = true;
                                controller.Rapid.Stop(ABB.Robotics.Controllers.RapidDomain.StopMode.Immediate);
                                tasks[0].ResetProgramPointer();
                            }
                           


                        }
                        catch (System.InvalidOperationException ex)
                        {
                            MessageBox.Show("Mastership is held by another client." + ex.Message);
                            fr.LogMessage(ex.Message + ex.Source + ex.StackTrace);
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show("Unexpected error occurred: " + ex.Message);
                            fr.LogMessage(ex.Message + ex.Source + ex.StackTrace);
                        }

                    }

                }
               
                
                
            }
            catch (System.Exception ex)
            {
                
                MessageBox.Show("An Error Has Occurred: " + ex.Message);
                fr.LogMessage(ex.Message + ex.Source + ex.StackTrace);


            }

         
        }
        public void Continue()
        {
            Bool _waiting = (Bool)controllerWaiting.Value;
            if (_waiting == true)
            {
                //Things to do when robot is waiting 
                gphoto.CaptureAndDownload();
                using (m = Mastership.Request(controller.Rapid))
                {
                    controllerWaiting.Value = new Bool(false);
                }
            }
        }
       
// Event Handlers
        private void ProgramPointer_Changed(object sender, ProgramPositionEventArgs e)
        {
            //The Below Line Logs the program pointer row for debugging
            //fr.LogMessage(tasks[0].ProgramPointer.Range.Begin.Row.ToString());
            Bool _waiting = (Bool)controllerWaiting.Value;
            if (tasks[0].ProgramPointer.Routine == "ControllerWait" && _waiting == true )
            {
                fr.LogMessage("Waiting");
            }
        }

        private void ControllerWaitCheck(object sender,DataValueChangedEventArgs e)
        {
            Bool _waiting = (Bool)controllerWaiting.Value;
            if (_waiting == true)
            {
                //Things to do when robot is waiting 
                //gphoto.CaptureAndDownload();
                                                                             
            }
        }
        //End Event Handlers


    }

}
