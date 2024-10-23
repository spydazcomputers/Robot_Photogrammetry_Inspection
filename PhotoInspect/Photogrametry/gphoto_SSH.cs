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
        public bool subRunning;
        
        private Process WSLproc;
        private bool WSLRunning=false;
        private SshClient sshCLI;
        private ShellStream sshStream;
        private string folder;
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
                //Check if we succeeded in starting the SSHShell
                if (subRunning)
                {
                    // Create a new folder in the Home Directory for this session and go there for capturing
                    string now = DateTime.Now.ToString("dd_MM_yy_HH_mm");
                    folder = $"~/camera/photos_{now}";
                    SSHWrite($"mkdir {folder}");
                    
                }
                else 
                {
                    //Add Start Failed Handler here
                }
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
                SSHWrite($"gphoto2 --trigger-capture");
                await WaitSeconds(fr.i_Interval);
            }



            //Thread.Sleep(100);
            //Download the Files to the RPI
            SSHWrite($"gphoto2 --force-overwrite --get-file {startfiles + 1}-{startfiles + fr.i_Frames}");
            //Thread.Sleep(5000);
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
                if (sshStream.CanWrite) 
                    { 
                    fr.SubRunning.BackColor = System.Drawing.Color.Green;
                    fr.SubRunning.Text = "Running";
                    subRunning = true;
                    }    
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
                string output = null;
                string command = $"cd {folder}; {_cmd}";
                SshCommand sc = sshCLI.CreateCommand(command);
                sc.Execute();
                output = sc.Result;
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

                    fr.SubRunning.BackColor = System.Drawing.Color.Red;
                    fr.SubRunning.Text = "Stopped";
                    subRunning = false;

                

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
                fr.LogMessage(ex.Message + ex.Source + ex.StackTrace);
            }
        }

        }
    }
