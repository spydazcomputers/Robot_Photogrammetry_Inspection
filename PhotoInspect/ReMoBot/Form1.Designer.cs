namespace RapidDataBinding
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.IPAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ControllerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_ScanCTRLS = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.ip4 = new System.Windows.Forms.TextBox();
            this.IP3 = new System.Windows.Forms.TextBox();
            this.IP2 = new System.Windows.Forms.TextBox();
            this.IP1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cycleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.executionTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.motionDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.motionPointerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.programPointerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remainingCyclesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btn_CTRL_Connect = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.btn_CTRL_Connect);
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Controls.Add(this.btn_ScanCTRLS);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.richTextBox2);
            this.groupBox1.Controls.Add(this.ip4);
            this.groupBox1.Controls.Add(this.IP3);
            this.groupBox1.Controls.Add(this.IP2);
            this.groupBox1.Controls.Add(this.IP1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(1, 39);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(901, 503);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IPAddress,
            this.ControllerName});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(10, 6);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(334, 97);
            this.listView1.TabIndex = 34;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // IPAddress
            // 
            this.IPAddress.Text = "IP Address";
            this.IPAddress.Width = 107;
            // 
            // ControllerName
            // 
            this.ControllerName.Text = "Controller Name";
            this.ControllerName.Width = 205;
            // 
            // btn_ScanCTRLS
            // 
            this.btn_ScanCTRLS.Location = new System.Drawing.Point(392, 83);
            this.btn_ScanCTRLS.Name = "btn_ScanCTRLS";
            this.btn_ScanCTRLS.Size = new System.Drawing.Size(139, 23);
            this.btn_ScanCTRLS.TabIndex = 33;
            this.btn_ScanCTRLS.Text = "Scan Controllers";
            this.btn_ScanCTRLS.UseVisualStyleBackColor = true;
            this.btn_ScanCTRLS.Click += new System.EventHandler(this.btn_ScanCTRLS_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(731, 330);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 28);
            this.button4.TabIndex = 31;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(392, 152);
            this.richTextBox2.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(463, 171);
            this.richTextBox2.TabIndex = 30;
            this.richTextBox2.Text = "";
            // 
            // ip4
            // 
            this.ip4.Location = new System.Drawing.Point(835, 386);
            this.ip4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ip4.Name = "ip4";
            this.ip4.Size = new System.Drawing.Size(41, 22);
            this.ip4.TabIndex = 29;
            // 
            // IP3
            // 
            this.IP3.Location = new System.Drawing.Point(788, 386);
            this.IP3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.IP3.Name = "IP3";
            this.IP3.Size = new System.Drawing.Size(41, 22);
            this.IP3.TabIndex = 28;
            // 
            // IP2
            // 
            this.IP2.Location = new System.Drawing.Point(741, 386);
            this.IP2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.IP2.Name = "IP2";
            this.IP2.Size = new System.Drawing.Size(41, 22);
            this.IP2.TabIndex = 27;
            // 
            // IP1
            // 
            this.IP1.Location = new System.Drawing.Point(693, 386);
            this.IP1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.IP1.Name = "IP1";
            this.IP1.Size = new System.Drawing.Size(41, 22);
            this.IP1.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(580, 390);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 23;
            this.label1.Text = "Controller IP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 21;
            this.label5.Text = "Event Log:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(240, 330);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(105, 30);
            this.button3.TabIndex = 20;
            this.button3.Text = "Save Log";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Enabled = false;
            this.richTextBox1.Location = new System.Drawing.Point(7, 152);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(337, 172);
            this.richTextBox1.TabIndex = 12;
            this.richTextBox1.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(8, 399);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(139, 57);
            this.button2.TabIndex = 10;
            this.button2.Text = "Start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(184, 402);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 50);
            this.button1.TabIndex = 9;
            this.button1.Text = "Stop";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cycleDataGridViewTextBoxColumn
            // 
            this.cycleDataGridViewTextBoxColumn.DataPropertyName = "Cycle";
            this.cycleDataGridViewTextBoxColumn.HeaderText = "Cycle";
            this.cycleDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.cycleDataGridViewTextBoxColumn.Name = "cycleDataGridViewTextBoxColumn";
            this.cycleDataGridViewTextBoxColumn.Width = 125;
            // 
            // enabledDataGridViewCheckBoxColumn
            // 
            this.enabledDataGridViewCheckBoxColumn.DataPropertyName = "Enabled";
            this.enabledDataGridViewCheckBoxColumn.HeaderText = "Enabled";
            this.enabledDataGridViewCheckBoxColumn.MinimumWidth = 6;
            this.enabledDataGridViewCheckBoxColumn.Name = "enabledDataGridViewCheckBoxColumn";
            this.enabledDataGridViewCheckBoxColumn.Width = 125;
            // 
            // executionTypeDataGridViewTextBoxColumn
            // 
            this.executionTypeDataGridViewTextBoxColumn.DataPropertyName = "ExecutionType";
            this.executionTypeDataGridViewTextBoxColumn.HeaderText = "ExecutionType";
            this.executionTypeDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.executionTypeDataGridViewTextBoxColumn.Name = "executionTypeDataGridViewTextBoxColumn";
            this.executionTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.executionTypeDataGridViewTextBoxColumn.Width = 125;
            // 
            // motionDataGridViewCheckBoxColumn
            // 
            this.motionDataGridViewCheckBoxColumn.DataPropertyName = "Motion";
            this.motionDataGridViewCheckBoxColumn.HeaderText = "Motion";
            this.motionDataGridViewCheckBoxColumn.MinimumWidth = 6;
            this.motionDataGridViewCheckBoxColumn.Name = "motionDataGridViewCheckBoxColumn";
            this.motionDataGridViewCheckBoxColumn.ReadOnly = true;
            this.motionDataGridViewCheckBoxColumn.Width = 125;
            // 
            // motionPointerDataGridViewTextBoxColumn
            // 
            this.motionPointerDataGridViewTextBoxColumn.DataPropertyName = "MotionPointer";
            this.motionPointerDataGridViewTextBoxColumn.HeaderText = "MotionPointer";
            this.motionPointerDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.motionPointerDataGridViewTextBoxColumn.Name = "motionPointerDataGridViewTextBoxColumn";
            this.motionPointerDataGridViewTextBoxColumn.ReadOnly = true;
            this.motionPointerDataGridViewTextBoxColumn.Width = 125;
            // 
            // programPointerDataGridViewTextBoxColumn
            // 
            this.programPointerDataGridViewTextBoxColumn.DataPropertyName = "ProgramPointer";
            this.programPointerDataGridViewTextBoxColumn.HeaderText = "ProgramPointer";
            this.programPointerDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.programPointerDataGridViewTextBoxColumn.Name = "programPointerDataGridViewTextBoxColumn";
            this.programPointerDataGridViewTextBoxColumn.ReadOnly = true;
            this.programPointerDataGridViewTextBoxColumn.Width = 125;
            // 
            // remainingCyclesDataGridViewTextBoxColumn
            // 
            this.remainingCyclesDataGridViewTextBoxColumn.DataPropertyName = "RemainingCycles";
            this.remainingCyclesDataGridViewTextBoxColumn.HeaderText = "RemainingCycles";
            this.remainingCyclesDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.remainingCyclesDataGridViewTextBoxColumn.Name = "remainingCyclesDataGridViewTextBoxColumn";
            this.remainingCyclesDataGridViewTextBoxColumn.Width = 125;
            // 
            // taskTypeDataGridViewTextBoxColumn
            // 
            this.taskTypeDataGridViewTextBoxColumn.DataPropertyName = "TaskType";
            this.taskTypeDataGridViewTextBoxColumn.HeaderText = "TaskType";
            this.taskTypeDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.taskTypeDataGridViewTextBoxColumn.Name = "taskTypeDataGridViewTextBoxColumn";
            this.taskTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.taskTypeDataGridViewTextBoxColumn.Width = 125;
            // 
            // typeDataGridViewTextBoxColumn
            // 
            this.typeDataGridViewTextBoxColumn.DataPropertyName = "Type";
            this.typeDataGridViewTextBoxColumn.HeaderText = "Type";
            this.typeDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
            this.typeDataGridViewTextBoxColumn.ReadOnly = true;
            this.typeDataGridViewTextBoxColumn.Width = 125;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 125;
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(899, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // btn_CTRL_Connect
            // 
            this.btn_CTRL_Connect.Location = new System.Drawing.Point(537, 83);
            this.btn_CTRL_Connect.Name = "btn_CTRL_Connect";
            this.btn_CTRL_Connect.Size = new System.Drawing.Size(75, 23);
            this.btn_CTRL_Connect.TabIndex = 35;
            this.btn_CTRL_Connect.Text = "Connect";
            this.btn_CTRL_Connect.UseVisualStyleBackColor = true;
            this.btn_CTRL_Connect.Click += new System.EventHandler(this.button5_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 535);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "ReMoBoT";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cycleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn executionTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn motionDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn motionPointerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn programPointerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn remainingCyclesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ip4;
        private System.Windows.Forms.TextBox IP3;
        private System.Windows.Forms.TextBox IP2;
        private System.Windows.Forms.TextBox IP1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btn_ScanCTRLS;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader IPAddress;
        private System.Windows.Forms.ColumnHeader ControllerName;
        private System.Windows.Forms.Button btn_CTRL_Connect;
    }
}

