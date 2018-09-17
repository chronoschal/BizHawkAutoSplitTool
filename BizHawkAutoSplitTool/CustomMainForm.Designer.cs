namespace BizHawk.Client.EmuHawk
{
    partial class CustomMainForm
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
            this.liveSplitConnectGroupBox = new System.Windows.Forms.GroupBox();
            this.connectionStatusLabel = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.portLabel = new System.Windows.Forms.Label();
            this.ipLabel = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.liveSplitConnectGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // liveSplitConnectGroupBox
            // 
            this.liveSplitConnectGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.liveSplitConnectGroupBox.Controls.Add(this.connectionStatusLabel);
            this.liveSplitConnectGroupBox.Controls.Add(this.connectButton);
            this.liveSplitConnectGroupBox.Controls.Add(this.portLabel);
            this.liveSplitConnectGroupBox.Controls.Add(this.ipLabel);
            this.liveSplitConnectGroupBox.Controls.Add(this.portTextBox);
            this.liveSplitConnectGroupBox.Controls.Add(this.ipTextBox);
            this.liveSplitConnectGroupBox.Location = new System.Drawing.Point(12, 13);
            this.liveSplitConnectGroupBox.Name = "liveSplitConnectGroupBox";
            this.liveSplitConnectGroupBox.Size = new System.Drawing.Size(582, 176);
            this.liveSplitConnectGroupBox.TabIndex = 0;
            this.liveSplitConnectGroupBox.TabStop = false;
            this.liveSplitConnectGroupBox.Text = "LiveSplit Connection";
            // 
            // connectionStatusLabel
            // 
            this.connectionStatusLabel.AutoSize = true;
            this.connectionStatusLabel.Location = new System.Drawing.Point(154, 97);
            this.connectionStatusLabel.Name = "connectionStatusLabel";
            this.connectionStatusLabel.Size = new System.Drawing.Size(151, 25);
            this.connectionStatusLabel.TabIndex = 5;
            this.connectionStatusLabel.Text = "Not connected";
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(6, 92);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(142, 34);
            this.connectButton.TabIndex = 4;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // portLabel
            // 
            this.portLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(476, 26);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(51, 25);
            this.portLabel.TabIndex = 3;
            this.portLabel.Text = "Port";
            // 
            // ipLabel
            // 
            this.ipLabel.AutoSize = true;
            this.ipLabel.Location = new System.Drawing.Point(7, 29);
            this.ipLabel.Name = "ipLabel";
            this.ipLabel.Size = new System.Drawing.Size(31, 25);
            this.ipLabel.TabIndex = 2;
            this.ipLabel.Text = "IP";
            // 
            // portTextBox
            // 
            this.portTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.portTextBox.Location = new System.Drawing.Point(476, 57);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(100, 31);
            this.portTextBox.TabIndex = 1;
            this.portTextBox.TextChanged += new System.EventHandler(this.portTextBox_TextChanged);
            // 
            // ipTextBox
            // 
            this.ipTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ipTextBox.Location = new System.Drawing.Point(6, 57);
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.Size = new System.Drawing.Size(464, 31);
            this.ipTextBox.TabIndex = 0;
            this.ipTextBox.TextChanged += new System.EventHandler(this.ipTextBox_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 195);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(576, 489);
            this.panel1.TabIndex = 1;
            // 
            // CustomMainForm
            // 
            this.ClientSize = new System.Drawing.Size(606, 809);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.liveSplitConnectGroupBox);
            this.Name = "CustomMainForm";
            this.Text = "Auto Split";
            this.liveSplitConnectGroupBox.ResumeLayout(false);
            this.liveSplitConnectGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox liveSplitConnectGroupBox;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.TextBox ipTextBox;
        private System.Windows.Forms.Label ipLabel;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label connectionStatusLabel;
        private System.Windows.Forms.Panel panel1;
    }
}