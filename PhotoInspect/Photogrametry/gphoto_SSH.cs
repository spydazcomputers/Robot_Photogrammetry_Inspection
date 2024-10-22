using Renci.SshNet;
using Renci.SshNet.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Photogrametry
{
    
    class gphoto_SSH
    {
        public Form1 fr;
        public RapidFunctions Rap;
        private Process WSLproc;
        private bool WSLRunning=false;
        private SshClient sshCLI;
        private ShellStream sshStream;
        public gphoto_SSH(Form1 _form1)
        {
            this.fr = _form1;

        }
       // The below function starts the windows subsystem for linux Binds the camera hardware and returns
       // a process item that can be used to access the WSL from the application.
        public void SubSystemStart() 
        {
            try
            {

                    SSHStart();
                    
                 // Create a new folder in the Home Directory for this session and go there for capturing
                    SSHWrite("cd ~/camera");
                    string now = DateTime.Now.ToString("dd_MM_yy_HH_mm");
                    SSHWrite($"mkdir photos_{now}");
                    SSHWrite($"cd photos_{now}");
            }
            catch(System.Exception ex) 
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
                fr.LogMessage(ex.Message + ex.Source + ex.StackTrace);
            }

        }
       
        //This Function kills the Kill the ssh system that was started
        public void SubSystemStop()
        {
            try
            {
                SSHEnd();
                
            }

            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
                fr.LogMessage(ex.Message + ex.Source + ex.StackTrace);
            }
        }
        static async Task WaitSeconds(int milliseconds)
        {
            await Task.Delay(milliseconds);
        }
       //Read the number of photos on the camera to an integer for determining which files to download
        public int GetCameraCount()
        {
            try
            {
                string Camera_Response;
                Camera_Response = SSHWrite($"gphoto2 --num-files -f={fr.CameraFolder.Text}");
                Match match = Regex.Match(Camera_Response, @"\d+$");
                if (match.Success)
                {
                    return int.Parse(match.Value);
                }
                return 0;
            
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
                fr.LogMessage(ex.Message + ex.Source + ex.StackTrace);
                return 0;
            }
        }
        public async void CaptureAndDownload()
        {
            int startfiles;
            //Find out how many photos are on the camera (Used to download at the end)
            startfiles = GetCameraCount();
            
            for (int i = 0; i < fr.i_Frames; i++)
            {
                //Capture the photos (Trigger capture is the fastest and we don't need other options)
                SSHWrite($"gphoto2 --trigger-capture");
                //Wait the defined interval
                await WaitSeconds(fr.i_Interval);
            }
            //Download the Files to the RPI
            SSHWrite($"gphoto2 --get-file {startfiles + 1}-{startfiles + fr.i_Frames}");

            //Add SFTP code here to download the files to the local computer and delete from PI

        }

        // This Function connects to the Pi
       public void SSHStart()
        {
            try
            {
                //Connect to the pi as gphoto user
                sshCLI = new SshClient(fr.ssh_IpAddress.Text, "gphoto", "gphoto");
                //Connect To SSH
                sshCLI.Connect();
                //Start the CLI Stream
                sshStream = sshCLI.CreateShellStream("input", 0, 0, 0, 0, 1000000);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
                fr.LogMessage(ex.Message + ex.Source + ex.StackTrace);
            }
        }

        public string SSHWrite(string _cmd)
        {
            try
            {
                sshStream.WriteLine(_cmd);
                string output = sshStream.Read();
                Thread.Sleep(100);
                Console.WriteLine(output);
                return output;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
                fr.LogMessage(ex.Message + ex.Source + ex.StackTrace);
                return "failed";
            }
        }
        public void SSHEnd()
        {
            try
            {
                sshStream.Dispose();
                sshCLI.Disconnect();
                sshCLI.Dispose();


            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
                fr.LogMessage(ex.Message + ex.Source + ex.StackTrace);
            }
        }

        }
    }
