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
using ABB.Robotics.Internal;


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
        Vive vr;
        private bool _run;
        bool _sendL, _sendR, _posenewR, _posenewL;
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

        public RapidFunctions(Form1 _form1, Vive _vr)
        {
            this.fr = _form1;
            this.vr = _vr;
        }

        //Sub Thread that Run's our Core Functions
        public void DataCollMain()
        {
            try
            {
                createcontroller();
                if (vr.InitVive())
                    GetData();
                else
                {
                    Stop();
                    string msg = "Vive is Not Alive Stop Called";
                    fr.LogMessage(msg);

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
                fr.LogMessage(ex.Message + ex.Source + ex.StackTrace);
            }

        }

  

        //Connect to the Robo Studio Controller

        public void createcontroller()
        {
            try
            {
                // Create A Robo Studio Controller Connector
                this.objNetworkWatcher = new NetworkScanner();
                objNetworkWatcher.Scan();
                ControllerInfoCollection objControllerInfoCollection = objNetworkWatcher.Controllers;
                if (objNetworkWatcher.Controllers.Count != 0)
                {
                    for (int i = 0; i < objControllerInfoCollection.Count; i++)
                    {
                        if (objControllerInfoCollection[i].IPAddress.ToString() == this.IP && this.IP != "..." )
                        {
                            objController = new Controller(objControllerInfoCollection[i]);
                            objController.Logon(UserInfo.DefaultUser);
                        }
                        else
                        {
                            objController = new Controller(objControllerInfoCollection[i]);
                            objController.Logon(UserInfo.DefaultUser);
                        }
                    }
                
               
                }
                else
                {
                    fr.LogMessage("No Controller Found Stop Called");
                    Stop();
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
                   // tasks[2].ResetProgramPointer();
                    tasks[1].ResetProgramPointer();
                    //tasks[1].Enabled = false;
                    tasks[0].ResetProgramPointer();
                    

                } else
                {
                   
                    AutoClosingMessageBox.Show("Controller Not in Auto Stop() called", "Controller Status", 5000);
                    Stop();
                }
                //While the program has not been told to stop read from the stream and update the robostudio variables
                while (_run)
                {
                    this.ProcessButtons();
                    //Perform operation 
                    if (objController.Rapid.ExecutionStatus != ExecutionStatus.Running && vr.markerVal != 0)
                        objController.Rapid.Start();
                    CheckForPose();
                  //  prev_Robtarget.Fill2(null);
                  //  new_Robtarget.Fill2(null);
                   
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

            //Check Which hand we are updateing pose_[0] = 0 is left, 1000 is right
            if (_posenewL == true )
            {
                vr.PoseDiff((int)vr.LEFT);
                vr.pose_quat_left = vr.GetQuaterion(vr.current_pose_left);

                double comparetor;
                vr.PoseDiffRelative((int)vr.LEFT);

                comparetor = Math.Sqrt(Math.Pow(vr.pose_diff_leftRel.m11 * 250, 2) + Math.Pow(vr.pose_diff_leftRel.m3 * 250, 2) + Math.Pow(vr.pose_diff_leftRel.m7 * -250, 2));

                if (comparetor > minimumMove)
                {
                    //update the new robtarget with the previous xyz + the new pose data * 1000 to convert meters to millimeters

                    new_RobtargetL.Trans.X = prev_RobtargetL.Trans.X + (vr.pose_diff_left.m11 * 250);
                    new_RobtargetL.Trans.Y = prev_RobtargetL.Trans.Y + (vr.pose_diff_left.m3 * 250);
                    new_RobtargetL.Trans.Z = prev_RobtargetL.Trans.Z + (vr.pose_diff_left.m7 * -250);

                    //update the quaterions of the rob target   
                    /*
                    new_RobtargetL.Rot.Q1 = vr.pose_quat_left.w;
                    new_RobtargetL.Rot.Q2 = vr.pose_quat_left.z;
                    new_RobtargetL.Rot.Q3 = vr.pose_quat_left.x;
                    new_RobtargetL.Rot.Q4 = vr.pose_quat_left.y;


                    /*static rotations*/
                    new_RobtargetL.Rot.Q1 = prev_RobtargetL.Rot.Q1;
                    new_RobtargetL.Rot.Q2 = prev_RobtargetL.Rot.Q2;
                    new_RobtargetL.Rot.Q3 = prev_RobtargetL.Rot.Q3;
                    new_RobtargetL.Rot.Q4 = prev_RobtargetL.Rot.Q4;


                    new_RobtargetL.Extax.FillFromString2("[152,9E+09,9E+09,9E+09,9E+09,9E+09]");

                    data7.StringValue = new_RobtargetL.ToString();
                    fr.LogMessage("Left Target Updated to: " + new_RobtargetL.ToString());
                    vr.previous_pose_leftRel = vr.current_pose_left;
                }
                    _posenewL = false;

                


            }
            else if (_posenewR == true && moveCMPTR == 0)
            {
                vr.PoseDiff((int)vr.RIGHT);
                vr.pose_quat_right = vr.GetQuaterion(vr.current_pose_right);
                data8 = objController.Rapid.GetTask("T_ROB_R").GetModule("MainModule").GetRapidData("currposR");
                data8.ValueChanged += new EventHandler<DataValueChangedEventArgs>(data8_ValueChanged);
                //Fill the Previous rob target from the p30 target on the controller

                prev_RobtargetR.FillFromString2(data8.Value.ToString());

                new_RobtargetR.Trans.X = prev_RobtargetR.Trans.X + (vr.pose_diff_left.m11 * 1000);
                new_RobtargetR.Trans.Y = prev_RobtargetR.Trans.Y + (vr.pose_diff_left.m3 * -1000);
                new_RobtargetR.Trans.Z = prev_RobtargetR.Trans.Z + (vr.pose_diff_left.m7 * 1000);

                //update the quaterions of the rob target   
                /*
                new_Robtarget.Rot.Q1 = vr.pose_quat_left.w;
                new_Robtarget.Rot.Q2 = vr.pose_quat_left.x;
                new_Robtarget.Rot.Q3 = vr.pose_quat_left.z;
                new_Robtarget.Rot.Q4 = vr.pose_quat_left.y;

                /* Static Rotations*/
                new_RobtargetR.Rot.Q1 = prev_RobtargetR.Rot.Q1;
                new_RobtargetR.Rot.Q2 = prev_RobtargetR.Rot.Q2;
                new_RobtargetR.Rot.Q3 = prev_RobtargetR.Rot.Q3;
                new_RobtargetR.Rot.Q4 = prev_RobtargetR.Rot.Q4;

                //new_Robtarget.Robconf.FillFromString2("[-1,0,-1,11]");
                new_RobtargetR.Extax.FillFromString2("[152,9E+09,9E+09,9E+09,9E+09,9E+09]");
                data8 = objController.Rapid.GetTask("T_ROB_R").GetModule("MainModule").GetRapidData("p30R");
                data8.StringValue = new_RobtargetR.ToString();
              //  fr.LogMessage("Right Target Updated to: " + new_Robtarget.ToString());

                vr.previous_pose_right = vr.current_pose_right;

                _posenewR = false;
            }






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
                if (sendRthread != null )
                {
                    _sendR = false;
                    Thread.Sleep(500);
                    if (sendRthread.IsAlive)
                        sendRthread.Abort();
                }

                if (sendLthread != null)
                {
                    _sendL = false;
                    Thread.Sleep(500);
                    if (sendLthread.IsAlive)
                        sendLthread.Abort();
                }
                //shutdown vr
                vr.VRShutdown();
                //If Send Data Thread didn't exit kill it
                
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
                
                m.Release();
                
            }
            catch (System.Exception ex)
            {
                
                MessageBox.Show("An Error Has Occurred: " + ex.Message);
                fr.LogMessage(ex.Message + ex.Source + ex.StackTrace);


            }

        }

        public bool ProcessButtons()
        {
            try
            {
                
                vr.markerVal = 0;
                vr.PollButtons();
                // this.markerVal;
                switch (vr.markerVal)
                {
                    case (UInt32)Vive.ButtonDefines.Right_Trigger_Touch:
                        if (_sendR == false)
                        {
                            vr.PollVive(vr.RIGHT);
                            vr.previous_pose_right = vr.current_pose_right;
                            data7 = objController.Rapid.GetTask("T_ROB_R").GetModule("MainModule").GetRapidData("p20R");
                            data7.ValueChanged += new EventHandler<DataValueChangedEventArgs>(data7_ValueChanged);
                            //Fill the Previous rob target from the p30 target on the controller

                            prev_RobtargetR.FillFromString2(data7.Value.ToString());
                            data7 = objController.Rapid.GetTask("T_ROB_R").GetModule("MainModule").GetRapidData("p30R");

                            _sendR = true;
                            sendRthread = new Thread(new ThreadStart(this.UpdateRightData));
                            sendRthread.Name = "Update Right Pose";
                            sendRthread.Start();

                        }
                        break;
                    case (UInt32)Vive.ButtonDefines.Right_Trigger_UnTouch:
                        this._sendR = false;
                        if (this.sendRthread != null)
                        this.sendRthread.Join();
                       

                        break;
                    case (UInt32)Vive.ButtonDefines.Left_Trigger_Touch:
                        if (_sendL == false)
                        {
                            vr.PollVive(vr.LEFT);
                            vr.previous_pose_left = vr.current_pose_left;
                            data7 = objController.Rapid.GetTask("T_ROB_L").GetModule("MainModule").GetRapidData("p20L");
                            data7.ValueChanged += new EventHandler<DataValueChangedEventArgs>(data7_ValueChanged);
                            //Fill the Previous rob target from the p30 target on the controller

                            prev_RobtargetL.FillFromString2(data7.Value.ToString());
                            data7 = objController.Rapid.GetTask("T_ROB_L").GetModule("MainModule").GetRapidData("p30L");
                            _sendL = true;
                            sendLthread = new Thread(new ThreadStart(this.UpdateLeftData));
                            sendLthread.Name = "Update Left Pose";
                            sendLthread.Start();
                        }
                        break;
                    case (UInt32)Vive.ButtonDefines.Left_Trigger_UnTouch:
                        this._sendL = false;
                        this._posenewL = false;

                        if (this.sendLthread != null)
                        this.sendLthread.Join();
                       
                        
                        
                        break;
                    case (UInt32)Vive.ButtonDefines.Left_GripButton_Touch:
                        vr.PollVive(vr.LEFT);
                        vr.previous_pose_left = vr.current_pose_left;
                        
                            data6.Value = new Num(0);

                        break;
                    case (UInt32)Vive.ButtonDefines.Right_GripButton_Touch:
                        vr.PollVive(vr.RIGHT);
                        vr.previous_pose_right = vr.current_pose_right;
                        data5.Value = new Num(0);

                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
                fr.LogMessage(ex.Message + ex.Source + ex.StackTrace);
                return false;
            }
        }
        public void UpdateRightData()
        {
            try
            {
                _posenewR = false;
                while (_sendR == true)
                {
                    if (_posenewR  == false)
                    {
                        vr.PollVive(vr.RIGHT);
                       s = System.String.Format("Pose: 1, {0}, {1}, {2}, {3}, {4}, {5}, {6}", vr.pose_diff_right.m3, vr.pose_diff_right.m7, vr.pose_diff_right.m11, vr.pose_quat_right.w, vr.pose_quat_right.x, vr.pose_quat_right.y, vr.pose_quat_right.z);
                        _posenewR = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("An Error Has Occurred: " + ex.Message);
                fr.LogMessage(ex.Message + ex.Source + ex.StackTrace);

            }
        }


        public void UpdateLeftData()
        {
            try
            {
                _posenewL = false;
                while (_sendL == true)
                {
                    if (_posenewL == false)
                    {
                        vr.PollVive(vr.LEFT);
                        vr.PoseDiff((int)vr.LEFT);
                        s = System.String.Format("Pose: 0, {0}, {1}, {2}, {3}, {4}, {5}, {6}", vr.current_pose_left.m3, vr.current_pose_left.m7, vr.current_pose_left.m11, vr.pose_quat_left.w, vr.pose_quat_left.x, vr.pose_quat_left.y, vr.pose_quat_left.z);
                       fr.LogMessage2(s);
                        _posenewL = true;

                    }
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
