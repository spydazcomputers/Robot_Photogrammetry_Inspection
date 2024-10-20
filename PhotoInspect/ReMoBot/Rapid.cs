using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ABB.Robotics;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.RapidDomain;
using ABB.Robotics.Controllers.MotionDomain;
using ABB.Robotics.Controllers.ConfigurationDomain;
using ABB.Robotics.Controllers.Discovery;


namespace RapidDataBinding
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

        public Form1 fr;
        public decimal waittime = 100;
        string s;
        public string IP;
        public Controller controller = null;
        public RapidFunctions(Form1 _form1)
        {
            this.fr = _form1;
            
        }

        //Sub Thread that Run's our Core Functions
        public void DataCollMain()
        {
            try
            {
                //createcontroller();
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
                fr.LogMessage(ex.Message + ex.Source + ex.StackTrace);
            }

        }

        //Controller Scanner
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

        public void createcontroller(ListViewItem CTRLSelect)
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
                    using (m = Mastership.Request(controller.Rapid))
                    {
                        //Perform operation
                        tasks[0].ResetProgramPointer();
                        tasks[0].ProgramPointerChanged += new EventHandler<ProgramPositionEventArgs>(ProgramPointer_Changed);
                        controller.Rapid.Start();
                    }
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
                //Wait 500ms for Send Data thread to exit
              
                //try to exit the send data loops clean if not abort
                
                if (objController != null)
                {
                    if (objController.Rapid.ExecutionStatus ==
                           ABB.Robotics.Controllers.RapidDomain.ExecutionStatus.Running)
                    {

                        try
                        {
                            if (m == null)
                                m = Mastership.Request(objController.Rapid);
                            m.ReleaseOnDispose = true;
                            objController.Rapid.Stop(ABB.Robotics.Controllers.RapidDomain.StopMode.Immediate);
                            
                           


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
                if(m != null)
                {
                    m.Release();
                }
                
                
            }
            catch (System.Exception ex)
            {
                
                MessageBox.Show("An Error Has Occurred: " + ex.Message);
                fr.LogMessage(ex.Message + ex.Source + ex.StackTrace);


            }

        }

       
                // Data Changed Event Handlers
        private void data1_ValueChanged(object sender, DataValueChangedEventArgs e)
        {

            string str = sender.ToString();


        }

        private void data2_ValueChanged(object sender, DataValueChangedEventArgs e)
        {

            string str = sender.ToString();


        }
        private void data3_ValueChanged(object sender, DataValueChangedEventArgs e)
        {

            string str = sender.ToString();


        }
        private void data4_ValueChanged(object sender, DataValueChangedEventArgs e)
        {

            string str = sender.ToString();


        }
        private void data5_ValueChanged(object sender, DataValueChangedEventArgs e)
        {

            string str = sender.ToString();


        }
        private void data6_ValueChanged(object sender, DataValueChangedEventArgs e)
        {

            string str = sender.ToString();


        }

        private void data7_ValueChanged(object sender, DataValueChangedEventArgs e)
        {

            string str = sender.ToString();


        }
        private void data8_ValueChanged(object sender, DataValueChangedEventArgs e)
        {

            string str = sender.ToString();


        }
        private void ProgramPointer_Changed(object sender, ProgramPositionEventArgs e)
        {
            fr.LogMessage(tasks[0].ProgramPointer.ToString());
        }
        //End Event Handlers


    }

}
