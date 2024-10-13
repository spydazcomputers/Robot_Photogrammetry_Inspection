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

        ABB.Robotics.Controllers.RapidDomain.RobTarget prev_RobtargetL, new_RobtargetL, prev_RobtargetR, new_RobtargetR;
        ABB.Robotics.Controllers.RapidDomain.Num _num1;
        private ABB.Robotics.Controllers.RapidDomain.Task[] tasks = null;
        Thread oThread, sendRthread, sendLthread;
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
        private Controller controller = null;
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
                        if (this.controller != null)
                        {
                            this.controller.Logoff();
                            this.controller.Dispose();
                            this.controller = null;
                        }
                        this.controller = ControllerFactory.CreateFrom(controllerInfo);
                        this.controller.Logon(UserInfo.DefaultUser);
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

        /// <summary>
        /// Main Data Collection Loop reads from stdout on the remobot thread and uses the incomming data to create a rob target 
        /// </summary>
        public void GetData()
        {
            try
            {
                //Take Control of the RobotController 
                m = Mastership.Request(objController.Rapid);
                if (objController.OperatingMode == ControllerOperatingMode.Auto)
                {
                    tasks = objController.Rapid.GetTasks();
                    fr.LogMessage(tasks[0].Name);
                    tasks[0].ResetProgramPointer();
                    

                } else
                {
                   
                    AutoClosingMessageBox.Show("Controller Not in Auto Stop() called", "Controller Status", 5000);
                    Stop();
                }
                //While the program has not been told to stop read from the stream and update the robostudio variables
                while (_run)
                {
                    
                    //Perform operation 
                    if (objController.Rapid.ExecutionStatus != ExecutionStatus.Running )
                        objController.Rapid.Start();
                   
                }

            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Mastership is held by another client." + ex.Message);
                fr.LogMessage(ex.Message + ex.Source + ex.StackTrace);
                Stop();
            }
            catch (System.Exception ex)
            {
                AutoClosingMessageBox.Show("Unexpected error occurred: " + ex.Message + ex.TargetSite, "LMS", 5000);
                fr.LogMessage(ex.Message + ex.Source + ex.StackTrace);
                Stop();
            }

        }
        //Start The Show
        public void Start()
        {
            try
            {
                _run = true;
                oThread = new Thread(new ThreadStart(this.DataCollMain));
                oThread.Name = "Data Coll";
                oThread.Start();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("An Error Has Occurred: " + ex.Message);
                fr.LogMessage(ex.Message + ex.Source + ex.StackTrace);
            }

        }

        public void CheckForPose()
        {



            data6 = objController.Rapid.GetTask("T_ROB_L").GetModule("MainModule").GetRapidData("moveCMPTL");
            data6.ValueChanged += new EventHandler<DataValueChangedEventArgs>(data6_ValueChanged);
            ABB.Robotics.Controllers.RapidDomain.Num _num1;
            _num1 = (ABB.Robotics.Controllers.RapidDomain.Num)data6.Value;
            double moveCMPTL = _num1;

            data5 = objController.Rapid.GetTask("T_ROB_R").GetModule("MainModule").GetRapidData("moveCMPTR");
            data5.ValueChanged += new EventHandler<DataValueChangedEventArgs>(data5_ValueChanged);
            ABB.Robotics.Controllers.RapidDomain.Num _num2;
            _num2 = (ABB.Robotics.Controllers.RapidDomain.Num)data5.Value;
            double moveCMPTR = _num2;

            
//            int wait = Convert.ToInt16(waittime);
//            Thread.Sleep(wait);




        }

        //Stop Everything
        public void Stop()
        {
            try
            {
                //Stop the Send data while loop
                _run = false;
                //Wait 500ms for Send Data thread to exit
                Thread.Sleep(500);
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
                            
                           


                            if (oThread != null && oThread.IsAlive && Thread.CurrentThread.Name != "Data Coll")
                                oThread.Abort();
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
        //End Event Handlers


    }

}
