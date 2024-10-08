

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Valve.VR;


namespace RapidDataBinding
{
    class Vive
    {
        // Pose matrices
        public HmdQuaternion_t pose_quat_left, pose_quat_right;
        public HmdMatrix34_t pose_diff_right, pose_diff_left, pose_diff_leftRel;
        public HmdMatrix34_t current_pose_left;
        public HmdMatrix34_t current_pose_right;
        public HmdMatrix34_t previous_pose_left, previous_pose_right, previous_pose_leftRel;
        
        // Device ID's of left and right controller
        public uint LEFT, RIGHT;
        
        // Button Value returned by PollButtons()
        public UInt32 markerVal;
        
        //Boolean set to true by InitVive 
        public bool ViveAlive = false;
       
        //ProcessButtons comparison list for markerVal
        public enum ButtonDefines
        {

            Right_Trigger_Touch = 37,
            Right_Trigger_Press = 35,
            Left_Trigger_Touch = 165,
            Left_Trigger_Press = 163,
            Right_Trigger_UnPress = 36,
            Right_Trigger_UnTouch = 38,
            Left_Trigger_UnPress = 164,
            Left_Trigger_UnTouch = 166,
            Right_Touchpad_Touch = 29,
            Right_Touchpad_Press = 27,
            Right_Touchpad_UnTouch = 30,
            Right_Touchpad_UnPress = 28,
            Left_Touchpad_Touch = 157,
            Left_Touchpad_Press = 155,
            Left_Touchpad_UnTouch = 158,
            Left_Touchpad_UnPress = 156,
            // The grip buttons on either side of a controller return the same value regardless of which on is pressed.
            // The grip buttons also return two values, touch and press, even though there is no way to discern a difference between touch and press
            Right_GripButton_Press = 23,
            Right_GripButton_Touch = 21,
            Left_GripButton_Press = 147,
            Left_GripButton_Touch = 149,
            Right_GripButton_UnPress = 20,
            Right_GripButton_UnTouch = 22,
            Left_GripButton_UnPress = 148,
            Left_GripButton_UnTouch = 150,
            // The ApplicationMenu Button returns two values
            Right_ApplicationMenu_Touch = 13,
            Right_ApplicationMenu_Press = 15,
            Left_ApplicationMenu_Touch = 141,
            Left_ApplicationMenu_Press = 143,
            Right_ApplicationMenu_UnTouch = 14,
            Right_ApplicationMenu_UnPress = 12,
            Left_ApplicationMenu_UnTouch = 142,
            Left_ApplicationMenu_UnPress = 140,
            //The button below the touchpad did not return a value when pressed. That is the only button that is not defined here.
        }
        
        // Vive handles
        private EVRInitError eError = EVRInitError.None;
        private CVRSystem vrsys;
        
        //Reference handle to update Form1
        Form1 fr;

       //Struct to hold the device indices
        public struct DeviceIndex
        {
            public UInt32 HMD;
            public UInt32 Left;
            public UInt32 Right;
        }
        public DeviceIndex DeviceList = new DeviceIndex();
    
        //Class Initilization Routine
        public Vive(Form1 _form1)
        {
            fr = _form1;
        }

        //Initiate our interaction with the vive
        public bool InitVive()
        {
            try
            {
                vrsys = OpenVR.Init(ref eError, EVRApplicationType.VRApplication_Other);
                if (eError != EVRInitError.None)
                {

                    return false;
                }



                vrsys.GetTrackedDeviceIndexForControllerRole(ETrackedControllerRole.LeftHand);
                DeviceList.HMD = OpenVR.k_unTrackedDeviceIndex_Hmd;
                DeviceList.Left = vrsys.GetTrackedDeviceIndexForControllerRole(ETrackedControllerRole.LeftHand);
                DeviceList.Right = vrsys.GetTrackedDeviceIndexForControllerRole(ETrackedControllerRole.RightHand);
                this.LEFT = DeviceList.Left;
                this.RIGHT = DeviceList.Right;

                fr.UpdateFormFields();

                this.ViveAlive = true;
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
                return false;
            }
        }

        //shut down our interaction with the Vive
        public void VRShutdown()
        {
            if(ViveAlive == true)
            OpenVR.Shutdown();
            this.ViveAlive = false;
        }

        //Read the Event buffer and update markerVal to check for button presses
        public bool PollButtons()
        {
            try
            {
                VREvent_t nextevent = new VREvent_t();

                vrsys.PollNextEvent(ref nextevent, (UInt32)System.Runtime.InteropServices.Marshal.SizeOf(nextevent));
                UInt32 devId = nextevent.trackedDeviceIndex;
                ETrackedDeviceClass devClass = vrsys.GetTrackedDeviceClass(devId);  // TrackedDeviceClass_Controller = 2
                if (devClass == ETrackedDeviceClass.Controller)

                {
                    // TrackedControllerRole_LeftHand or TrackedControllerRole_RightHand
                    ETrackedControllerRole ctrlRole = vrsys.GetControllerRoleForTrackedDeviceIndex(devId);

                    //0 = k_EButton_System; 1 = k_EButton_ApplicationMenu; 2 = k_EButton_Grip (either); 32 = k_EButton_Axis0 = k_EButton_SteamVR_Touchpad; 33 = k_EButton_Axis1 = k_EButton_SteamVR_Trigger
                    EVRButtonId butId = (EVRButtonId)nextevent.data.controller.button;

                    //VREvent_TrackedDeviceActivated = 100; VREvent_TrackedDeviceUserInteractionStarted = 103; VREvent_TrackedDeviceUserInteractionEnded = 104; VREvent_ButtonPress = 200; VREvent_ButtonUnpress = 201; VREvent_ButtonTouch = 202; VREvent_ButtonUntouch = 203;
                    EVREventType evType = (EVREventType)nextevent.eventType;

                    // We are interested in 2 controllers. Use 1-2 MSB (markerVal >> 7)
                    // We are interested in 4 buttons. Use 3-5 MSB ((markerVal % 128) >> 3)
                    // We are interested in 7 events. Use 6-8 MSB (markerVal % 8)

                    if (ctrlRole == ETrackedControllerRole.LeftHand)

                    {
                        markerVal |= (1 << 7);
                    }

                    UInt32 button = (UInt32)butId;
                    if (button >= 32)

                    {
                        button -= 29;
                    }
                    markerVal |= button << 3;

                    UInt32 newevent = (UInt32)evType;

                    newevent -= 100;
                    if (newevent >= 3)
                    {

                        newevent -= 2;
                    }
                    if (newevent >= 98)

                    {

                        newevent -= 95;
                    }
                    markerVal |= newevent;




                    //		std::cout
                    //		<< ((ctrlRole == vr::TrackedControllerRole_LeftHand) ? "Left" : "Right")
                    //			<< "; Button: " << vrsys->GetButtonIdNameFromEnum(butId)
                    //			<< "; Event: " << vrsys->GetEventTypeNameFromEnum(evType)
                    //			<< "; Marker Val: " << static_cast<int16_t>(markerVal)
                    //			<< std::endl;

                }

                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
                return false;
            }
        }

        //Read the Pose of a designated controller
        public bool PollVive(uint hand)
        {
            try
            {
                

                int device_id;  // Index into our list of 3 devices: HMD, Left, Right


                // send data forever
                TrackedDevicePose_t[] rTrackedDevicePose = new TrackedDevicePose_t[16];


                // Get the poses

                vrsys.GetDeviceToAbsoluteTrackingPose(ETrackingUniverseOrigin.TrackingUniverseStanding, 0.0f, rTrackedDevicePose);

                if (rTrackedDevicePose[hand].bPoseIsValid && rTrackedDevicePose[hand].bDeviceIsConnected)
                {


                    if (hand == DeviceList.Left)
                    {
                        
                        this.current_pose_left = rTrackedDevicePose[hand].mDeviceToAbsoluteTracking;
                        this.pose_quat_left = GetQuaterion(this.current_pose_left);
                        
                    }
                    else if (hand == this.RIGHT)
                    {
                        
                        this.current_pose_right = rTrackedDevicePose[hand].mDeviceToAbsoluteTracking;
                        
                    }
                   

                }
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
                return false;
            }
        }

        //Re-implementation of C++ copysign math function for use in calculation of quaternions
        private static float _copysign(float sizeval, float signval)
        {
            return Math.Sign(signval) == 1 ? Math.Abs(sizeval) : -Math.Abs(sizeval);
        }
   
     //Get the quaternion from the rotational part of the pose matrix
        public HmdQuaternion_t GetQuaterion(HmdMatrix34_t matrix)
        {
            HmdQuaternion_t q;
            q.w = Math.Sqrt(Math.Max(0, 1 + matrix.m0 + matrix.m5 + matrix.m10)) / 2;
            q.x = Math.Sqrt(Math.Max(0, 1 + matrix.m0 - matrix.m5 - matrix.m10)) / 2;
            q.y = Math.Sqrt(Math.Max(0, 1 - matrix.m0 + matrix.m5 - matrix.m10)) / 2;
            q.z = Math.Sqrt(Math.Max(0, 1 - matrix.m0 - matrix.m5 + matrix.m10)) / 2;
            q.x = _copysign((float)q.x, matrix.m9 - matrix.m6);
            q.y = _copysign((float)q.y, matrix.m2 - matrix.m8);
            q.z = _copysign((float)q.z, matrix.m4 - matrix.m1);
            return q;
        }

        //Find the differnce between two poses
        public void PoseDiff(int hand)
        {
            if ( hand == this.LEFT)
            {
                pose_diff_left.m0 = previous_pose_left.m0 - current_pose_left.m0;
                pose_diff_left.m1 = previous_pose_left.m1 - current_pose_left.m1;
                pose_diff_left.m2 = previous_pose_left.m2 - current_pose_left.m2;
                pose_diff_left.m3 = previous_pose_left.m3 - current_pose_left.m3;
                pose_diff_left.m4 = previous_pose_left.m4 - current_pose_left.m4;
                pose_diff_left.m5 = previous_pose_left.m5 - current_pose_left.m5;
                pose_diff_left.m6 = previous_pose_left.m6 - current_pose_left.m6;
                pose_diff_left.m7 = previous_pose_left.m7 - current_pose_left.m7;
                pose_diff_left.m8 = previous_pose_left.m8 - current_pose_left.m8;
                pose_diff_left.m9 = previous_pose_left.m9 - current_pose_left.m9;
                pose_diff_left.m10 = previous_pose_left.m10 - current_pose_left.m10;
                pose_diff_left.m11 = previous_pose_left.m11 - current_pose_left.m11;
            }
            else if (hand == this.RIGHT)
            {
                pose_diff_right.m0 = previous_pose_right.m0 - current_pose_right.m0;
                pose_diff_right.m1 = previous_pose_right.m1 - current_pose_right.m1;
                pose_diff_right.m2 = previous_pose_right.m2 - current_pose_right.m2;
                pose_diff_right.m3 = previous_pose_right.m3 - current_pose_right.m3;
                pose_diff_right.m4 = previous_pose_right.m4 - current_pose_right.m4;
                pose_diff_right.m5 = previous_pose_right.m5 - current_pose_right.m5;
                pose_diff_right.m6 = previous_pose_right.m6 - current_pose_right.m6;
                pose_diff_right.m7 = previous_pose_right.m7 - current_pose_right.m7;
                pose_diff_right.m8 = previous_pose_right.m8 - current_pose_right.m8;
                pose_diff_right.m9 = previous_pose_right.m9 - current_pose_right.m9;
                pose_diff_right.m10 = previous_pose_right.m10 - current_pose_right.m10;
                pose_diff_right.m11 = previous_pose_right.m11 - current_pose_right.m11;
            }
        }

        public void PoseDiffRelative(int hand)
        {
            if (hand == this.LEFT)
            {
                pose_diff_leftRel.m0 = previous_pose_leftRel.m0 - current_pose_left.m0;
                pose_diff_leftRel.m1 = previous_pose_leftRel.m1 - current_pose_left.m1;
                pose_diff_leftRel.m2 = previous_pose_leftRel.m2 - current_pose_left.m2;
                pose_diff_leftRel.m3 = previous_pose_leftRel.m3 - current_pose_left.m3;
                pose_diff_leftRel.m4 = previous_pose_leftRel.m4 - current_pose_left.m4;
                pose_diff_leftRel.m5 = previous_pose_leftRel.m5 - current_pose_left.m5;
                pose_diff_leftRel.m6 = previous_pose_leftRel.m6 - current_pose_left.m6;
                pose_diff_leftRel.m7 = previous_pose_leftRel.m7 - current_pose_left.m7;
                pose_diff_leftRel.m8 = previous_pose_leftRel.m8 - current_pose_left.m8;
                pose_diff_leftRel.m9 = previous_pose_leftRel.m9 - current_pose_left.m9;
                pose_diff_leftRel.m10 = previous_pose_leftRel.m10 - current_pose_left.m10;
                pose_diff_leftRel.m11 = previous_pose_leftRel.m11 - current_pose_left.m11;
            }
           /* else if (hand == this.RIGHT)
            {
                pose_diff_right.m0 = previous_pose_right.m0 - current_pose_right.m0;
                pose_diff_right.m1 = previous_pose_right.m1 - current_pose_right.m1;
                pose_diff_right.m2 = previous_pose_right.m2 - current_pose_right.m2;
                pose_diff_right.m3 = previous_pose_right.m3 - current_pose_right.m3;
                pose_diff_right.m4 = previous_pose_right.m4 - current_pose_right.m4;
                pose_diff_right.m5 = previous_pose_right.m5 - current_pose_right.m5;
                pose_diff_right.m6 = previous_pose_right.m6 - current_pose_right.m6;
                pose_diff_right.m7 = previous_pose_right.m7 - current_pose_right.m7;
                pose_diff_right.m8 = previous_pose_right.m8 - current_pose_right.m8;
                pose_diff_right.m9 = previous_pose_right.m9 - current_pose_right.m9;
                pose_diff_right.m10 = previous_pose_right.m10 - current_pose_right.m10;
                pose_diff_right.m11 = previous_pose_right.m11 - current_pose_right.m11;
            }*/
        }


    };

      
}
