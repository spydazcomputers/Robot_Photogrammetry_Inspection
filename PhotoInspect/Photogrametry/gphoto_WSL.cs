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
    
    class gphoto_WSL
    {
        public Form1 fr;
        public RapidFunctions Rap;
        private Process WSLproc;
        private bool WSLRunning=false;
        private SshClient sshCLI;
        private ShellStream sshStream;
        public gphoto_WSL(Form1 _form1)
        {
            this.fr = _form1;

        }
       // The below function starts the windows subsystem for linux Binds the camera hardware and returns
       // a process item that can be used to access the WSL from the application.
        public void SubSystemStart() 
        {
            try
            {

                    WSLproc = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = @"wsl.exe",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = false,
                            RedirectStandardInput = true,
                            CreateNoWindow = true,
                            // Arguments = "gphoto2 --capture-image-and-download -F=" + num_to_cap + " -I=" + interval + " --force-overwrite",
                        }
                    };

                    WSLproc.Start();
                    WSLproc.StandardInput.NewLine = "\n";
                    if (!WSLproc.HasExited)
                    {
                        WSLRunning = true;
                        fr.SubRunning.BackColor = System.Drawing.Color.Green;
                        fr.SubRunning.Text = "Running";
                    }

                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = "usbipd",
                        Arguments = $"attach --wsl --hardware-id {fr.s_hardwareID}",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    };

                    using (Process process = new Process { StartInfo = startInfo })
                    {
                        process.Start();
                        process.WaitForExit();
                    }

                    // Create a new folder in the Home Directory for this session and go there for capturing
                    WSLproc.StandardInput.WriteLine(@"cd ~/camera/");
                    string now = DateTime.Now.ToString("dd_MM_yy_HH_mm");
                    WSLproc.StandardInput.WriteLine($"mkdir photos_{now}");
                    WSLproc.StandardInput.WriteLine($"cd photos_{now}");
               
            }
            catch(System.Exception ex) 
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
                fr.LogMessage(ex.Message + ex.Source + ex.StackTrace);
            }

        }
       
        //This Function kills the WSL system that was started and detaches the USB
        public void SubSystemStop()
        {
            try
            {
                    if (WSLRunning == true)
                    {
                        if (fr.DeleteFilesOnExit.Checked == true)
                        {
                            WSLproc.StandardInput.WriteLine($"sudo gphoto2 --delete-all-files -f={fr.CameraFolder.Text}");
                        }
                        WSLproc.Kill();
                        WSLproc.Dispose();
                        WSLRunning = false;
                        fr.SubRunning.BackColor = System.Drawing.Color.Red;
                        fr.SubRunning.Text = "Not Running";
                    }

                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = "usbipd",
                        Arguments = $"detach --hardware-id {fr.s_hardwareID}",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using (Process process = new Process { StartInfo = startInfo })
                    {
                        process.Start();
                        process.WaitForExit();

                    }
                
                
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
                    WSLproc.StandardInput.WriteLine($"gphoto2 --num-files -f={fr.CameraFolder.Text}");
                    Camera_Response = WSLproc.StandardOutput.ReadLine();
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
            int startfiles, endfiles;
            
            // WSLproc.StandardInput.WriteLine("gphoto2 --capture-image-and-download");

            startfiles = GetCameraCount();
            for (int i = 0; i < fr.i_Frames; i++)
            {
                    WSLproc.StandardInput.WriteLine($"gphoto2 --trigger-capture");
                    await WaitSeconds(fr.i_Interval);
             
            }
                WSLproc.StandardInput.WriteLine($"gphoto2 --get-file {startfiles + 1}-{startfiles + fr.i_Frames}");
                        

        }

        }
    }
