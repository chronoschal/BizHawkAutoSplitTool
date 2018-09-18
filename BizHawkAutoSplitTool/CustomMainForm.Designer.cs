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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.triggersGroupBox = new System.Windows.Forms.GroupBox();
            this.resetButton = new System.Windows.Forms.Button();
            this.loadedProfileLabel = new System.Windows.Forms.Label();
            this.loadButton = new System.Windows.Forms.Button();
            this.triggerListBox = new System.Windows.Forms.ListBox();
            this.liveSplitConnectGroupBox.SuspendLayout();
            this.triggersGroupBox.SuspendLayout();
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
            this.liveSplitConnectGroupBox.Location = new System.Drawing.Point(12, 59);
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
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(606, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // triggersGroupBox
            // 
            this.triggersGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.triggersGroupBox.Controls.Add(this.triggerListBox);
            this.triggersGroupBox.Controls.Add(this.resetButton);
            this.triggersGroupBox.Controls.Add(this.loadedProfileLabel);
            this.triggersGroupBox.Controls.Add(this.loadButton);
            this.triggersGroupBox.Location = new System.Drawing.Point(12, 241);
            this.triggersGroupBox.Name = "triggersGroupBox";
            this.triggersGroupBox.Size = new System.Drawing.Size(582, 556);
            this.triggersGroupBox.TabIndex = 3;
            this.triggersGroupBox.TabStop = false;
            this.triggersGroupBox.Text = "Triggers";
            // 
            // resetButton
            // 
            this.resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.resetButton.Location = new System.Drawing.Point(6, 516);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(90, 34);
            this.resetButton.TabIndex = 2;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            // 
            // loadedProfileLabel
            // 
            this.loadedProfileLabel.AutoSize = true;
            this.loadedProfileLabel.Location = new System.Drawing.Point(182, 36);
            this.loadedProfileLabel.Name = "loadedProfileLabel";
            this.loadedProfileLabel.Size = new System.Drawing.Size(0, 25);
            this.loadedProfileLabel.TabIndex = 1;
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(6, 32);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(150, 34);
            this.loadButton.TabIndex = 0;
            this.loadButton.Text = "Load profile";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // triggerListBox
            // 
            this.triggerListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.triggerListBox.Font = new System.Drawing.Font("Consolas", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.triggerListBox.FormattingEnabled = true;
            this.triggerListBox.ItemHeight = 24;
            this.triggerListBox.Location = new System.Drawing.Point(7, 72);
            this.triggerListBox.Name = "triggerListBox";
            this.triggerListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.triggerListBox.Size = new System.Drawing.Size(569, 436);
            this.triggerListBox.TabIndex = 3;
            // 
            // CustomMainForm
            // 
            this.ClientSize = new System.Drawing.Size(606, 809);
            this.Controls.Add(this.triggersGroupBox);
            this.Controls.Add(this.liveSplitConnectGroupBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CustomMainForm";
            this.Text = "Auto Split";
            this.liveSplitConnectGroupBox.ResumeLayout(false);
            this.liveSplitConnectGroupBox.PerformLayout();
            this.triggersGroupBox.ResumeLayout(false);
            this.triggersGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox liveSplitConnectGroupBox;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.TextBox ipTextBox;
        private System.Windows.Forms.Label ipLabel;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label connectionStatusLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.GroupBox triggersGroupBox;
        private System.Windows.Forms.Label loadedProfileLabel;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.ListBox triggerListBox;
    }
}