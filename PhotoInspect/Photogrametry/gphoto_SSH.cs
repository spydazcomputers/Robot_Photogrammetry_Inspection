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
                    SSHWrite("cd ~/camera");
                    string now = DateTime.Now.ToString("dd_MM_yy_HH_mm");
                    SSHWrite($"mkdir photos_{now}");
                    SSHWrite($"cd photos_{now}");
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
                int index = Camera_Response.IndexOf($"Number of files in folder ");
                string subresponse = Camera_Response.Substring(index);
                int index2 = subresponse.IndexOf("\r");
                subresponse = subresponse.Substring(0, index2);
                Match match = Regex.Match(subresponse, @"\d+$");
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
            SSHWrite($"gphoto2 --shell");
            Thread.Sleep(1000);
            for (int i = 0; i < fr.i_Frames; i++)
            {

                
                //Capture the photos (Trigger capture is the fastest and we don't need other options)
                SSHWrite($"trigger-capture");
                //Wait the defined interval
                await WaitSeconds(fr.i_Interval);
            }
            SSHWrite("exit");
            Thread.Sleep(100);
            //Download the Files to the RPI
            SSHWrite($"gphoto2 --force-overwrite --get-file {startfiles + 1}-{startfiles + fr.i_Frames}");
            Thread.Sleep(5000);
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
                string EndOfCommandResponse = "EndOfOutput";
                string output = null;
                int index = -1;
                output = sshStream.Read();
                sshStream.Flush();
                //Don't add EndOfOutput to Async Commands
                if (_cmd != "gphoto2 --shell" && _cmd != "trigger-capture" && _cmd != "exit")
                {
                    sshStream.WriteLine(_cmd + $"; echo {EndOfCommandResponse}\n");
                }
                else
                {
                    sshStream.WriteLine(_cmd);
                }
                output = null;
                Thread.Sleep(100);
                while (index < 0)
                {
                    string output2 = sshStream.Read();
                    if (_cmd != "gphoto2 --shell" && _cmd != "trigger-capture" && _cmd != "exit")
                    {
                        index = output2.IndexOf($"\nEndOfOutput\r");
                        if (output2 != "")
                        {
                            Console.WriteLine(output2);
                            output += output2;
                        }
                        Thread.Sleep(100);

                    }
                    else
                    {
                        Thread.Sleep(500);
                        Console.WriteLine(output2);
                        break;
                    }
                }
                
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
