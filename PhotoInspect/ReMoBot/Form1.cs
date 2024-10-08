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
using Valve.VR;

namespace RapidDataBinding
{
    
      
    public partial class Form1 : Form
    {
        
        

        Valve.VR.IVRSystem newvr;
        RapidFunctions Rap;
        Vive vr;

        public Form1()
        {
                        
            InitializeComponent();
 //         createcontroller();               
            listmethod();
            vr = new Vive(this);
            Rap = new RapidFunctions(this, vr);
           

                

           
        }


        public void listmethod()
        {
           

        }
        
        public void LogMessage(string MSG)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.richTextBox1.AppendText(DateTime.Now.ToString() + ":   ");
            this.richTextBox1.AppendText(MSG);
            this.richTextBox1.AppendText("\n\r");
            this.richTextBox1.ScrollToCaret();
           
        }
        public void LogMessage2(string MSG)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.richTextBox2.AppendText(DateTime.Now.ToString() + ":   ");
            this.richTextBox2.AppendText(MSG);
            this.richTextBox2.AppendText("\n\r");
            this.richTextBox2.ScrollToCaret();

        }
        public void UpdateFormFields()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.Left_Device_ID.Text = vr.DeviceList.Left.ToString();
            this.Right_Device_ID.Text = vr.DeviceList.Right.ToString();
            this.HMD_Device_ID.Text = vr.DeviceList.HMD.ToString();
           
        }
/*
        public void ConnectNamedPipes()
        {
            // Connect to the Named Pipe Server on the Vive App
            // The connect function will indefinately wait for the pipe to become available
            // If that is not acceptable specify a maximum waiting time (in ms)

            pipeStream = new NamedPipeClientStream(".", "MyPipe", PipeDirection.InOut);

            if (!pipeStream.IsConnected)
            {
                pipeStream.Connect();
            }

            if (!pipeStream.IsConnected)    //It thinks it's connected but can't read anything ....
            {
                MessageBox.Show("Failed to connect ....");
                return;
            }

            //End Named Pipes Connection
        }
  
    */

        void OnRowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Rap.Stop();
            this.richTextBox1.Enabled = true;
            this.richTextBox1.ReadOnly = true;

        }
        
        public void UpdateGrid(string msg)
        {
            this.richTextBox1.Text = msg;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Enabled = false;
            this.richTextBox1.ReadOnly = false;
            Rap.IP = this.IP1.Text.ToString() + '.' + this.IP2.Text.ToString() + '.' + IP3.Text.ToString() + '.' + ip4.Text.ToString();
            Rap.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
        
        }

        
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.richTextBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.richTextBox1.Invoke(d, new object[] { text });
            }
            else
            {
                this.richTextBox1.Text = text;
            }
        }
        delegate void SetTextCallback(string text);

        public void ThreadProcSafe(string MSG)
        {
            this.SetText(MSG);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog file = new SaveFileDialog();
            file.Filter = "log files (*.log)|*.txt|All files (*.*)|*.*";
            file.DefaultExt = ".log";

            if (file.ShowDialog() == DialogResult.OK)
            this.richTextBox1.SaveFile(file.FileName, RichTextBoxStreamType.PlainText);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created By Ryan Mecham and David Sanabria\n"
                + "In collaboration with:\n"
                + "Professor Dr. Haoyu Wang, CCSU and\n"
                + "Dr. Biao Zhang, ABB Robotics\n\n" 
                + "Research Funding Provided by CT Space Grant NASA research grant Summer 2016");
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog file = new SaveFileDialog();
            file.Filter = "log files (*.log)|*.txt|All files (*.*)|*.*";
            file.DefaultExt = ".log";

            if (file.ShowDialog() == DialogResult.OK)
                this.richTextBox2.SaveFile(file.FileName, RichTextBoxStreamType.PlainText);
        }
    }


}

public class AutoClosingMessageBox
{
    System.Threading.Timer _timeoutTimer;
    string _caption;
    AutoClosingMessageBox(string text, string caption, int timeout)
    {
        _caption = caption;
        _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
            null, timeout, System.Threading.Timeout.Infinite);
        MessageBox.Show(text, caption);
    }

    public static void Show(string text, string caption, int timeout)
    {
        new AutoClosingMessageBox(text, caption, timeout);
    }

    void OnTimerElapsed(object state)
    {
        IntPtr mbWnd = FindWindow(null, _caption);
        if (mbWnd != IntPtr.Zero)
            SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        _timeoutTimer.Dispose();
    }
    const int WM_CLOSE = 0x0010;
    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
    static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
}