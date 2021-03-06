﻿using BizHawk.Emulation.Common;
using BizHawkAutoSplitTool;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BizHawk.Client.EmuHawk
{
    public partial class CustomMainForm : Form, IExternalToolForm, IToolFormAutoConfig
    {
        public CustomMainForm()
        {
            InitializeComponent();
        }

        [RequiredService]
        private IMemoryDomains MemoryDomains { get; set; }

        [OptionalService]
        private IDebuggable Debuggable { get; set; }

        [ConfigPersist]
        private string LastConnectionAttemptIp { get; set; }

        [ConfigPersist]
        private int LastConnectionAttemptPort { get; set; }

        /// <summary>
        /// Return true if you want the <see cref="UpdateValues"/> method
        /// to be called before rendering
        /// </summary>
        public bool UpdateBefore => false;

        public bool AskSaveChanges() => true;

        /// <summary>
        /// This method is called when a frame is rendered
        /// You can comapre it the lua equivalent emu.frameadvance()
        /// </summary>
        public void UpdateValues()
        {
            m_timerSession?.OnFrame();
        }

        /// <summary>
        /// This method is called instead of regular <see cref="UpdateValues"/>
        /// when emulator is runnig in turbo mode
        /// </summary>
        public void FastUpdate()
        {
            m_timerSession?.OnFrame();
        }

        public void NewUpdate(ToolFormUpdateType type)
        { }

        /// <summary>
        /// Restart is called when a core boots
        /// </summary>
        public void Restart()
        {
            if (m_timerSession != null)
            {
                m_timerSession.Dispose();
            }

            m_timerSession = new TimerSession(m_splitProfile, MemoryDomains, Debuggable);

            //Debuggable.MemoryCallbacks.Add(new MemoryCallback(MemoryDomains.MainMemory.Name, MemoryCallbackType.Write, "", OnMemoryWrite, 0x079c, null));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ipTextBox.Text = LastConnectionAttemptIp;
            portTextBox.Text = LastConnectionAttemptPort.ToString();
        }

        private delegate void InvokeDelegate();

        private void connectButton_Click(object sender, EventArgs e)
        {
            if ((m_liveSplitConnectionSession == null) || (m_liveSplitConnectionSession.State == LiveSplitConnectionSessionState.Complete))
            {
                var ip = ipTextBox.Text;
                var port = int.Parse(portTextBox.Text);

                LastConnectionAttemptIp = ip;
                LastConnectionAttemptPort = port;

                m_liveSplitConnectionSession = new LiveSplitConnectionSession(ip, port);
                m_liveSplitConnectionSession.StateChanged += LiveSplitSession_StateChanged;
                m_liveSplitConnectionSession.Connect();
            }
            else if (m_liveSplitConnectionSession.State == LiveSplitConnectionSessionState.Connected)
            {
                m_liveSplitConnectionSession.Disconnect();
            }
            UpdateConnectionState();
        }

        private void ipTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateConnectButtonState();
        }

        private void portTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateConnectButtonState();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            //(0x079c, [(0x079b, 0x45), (0x079c, 0xdf)]),  # Start
            //(0x0a42, [(0x0a42, 0x13)]),                  # Ceres elevator
            //(0x1c1f, [(0x1c1f, 0x09)]),                  # Morph ball
            //(0x1c1f, [(0x1c1f, 0x02)]),                  # Missiles
            //(0x1c1f, [(0x1c1f, 0x13)]),                  # Bombs
            //(0x1c1f, [(0x1c1f, 0x03)]),                  # Super missiles
            //(0x1c1f, [(0x1c1f, 0x0e)]),                  # Charge beam                 
            //(0x079c, [(0x079b, 0x9f), (0x079c, 0xa5)]),  # Kraid room
            //(0x1c1f, [(0x1c1f, 0x07)]),                  # Varia Suit
            //(0x1c1f, [(0x1c1f, 0x0b)]),                  # Hi-Jump Boots
            //(0x1c1f, [(0x1c1f, 0x0d)]),                  # Speed Booster
            //(0x1c1f, [(0x1c1f, 0x10)]),                  # Wave Beam
            //(0x1c1f, [(0x1c1f, 0x0f)]),                  # Ice beam
            //(0x1c1f, [(0x1c1f, 0x04)]),                  # Power bomb
            //(0x079c, [(0x079b, 0x13), (0x079c, 0xcd)]),  # Phantoon room
            //(0x1c1f, [(0x1c1f, 0x1a)]),                  # Gravity Suit
            //(0x079c, [(0x079b, 0x5e), (0x079c, 0xd9)]),  # Botwoon's room
            //(0x079c, [(0x079b, 0x60), (0x079c, 0xda)]),  # Draygon's room
            //(0x1c1f, [(0x1c1f, 0x0c)]),                  # Space jump
            //(0x1c1f, [(0x1c1f, 0x12)])]                  # Plasma beam

            m_splitProfile = new SplitProfile(
                "Super Metroid",
                new List<MemoryTrigger>
                {
                    new MemoryTrigger(0x079c, new List<Tuple<uint, uint>>
                    {
                        new Tuple<uint, uint>(0x079c, 0xdf),
                        new Tuple<uint, uint>(0x079b, 0x45),
                    }),
                    new MemoryTrigger(0x0a42, new List<Tuple<uint, uint>>
                    {
                        new Tuple<uint, uint>(0x0a42, 0x13),
                    }),
                    new MemoryTrigger(0x1c1f, new List<Tuple<uint, uint>>
                    {
                        new Tuple<uint, uint>(0x1c1f, 0x09),
                    }),
                });

            triggerListBox.Items.Clear();
            foreach (var t in m_splitProfile.SplitTriggerList)
            {
                var isCurrentTrigger = (m_timerSession?.CurrentSplitIndex == triggerListBox.Items.Count);

                var triggerString = String.Format("{0}0x{1:x4}", isCurrentTrigger ? "--> " : "    ", t.TriggerAddress);
                foreach (var v in t.ExpectedMemoryValues)
                {
                    triggerString += string.Format(" (0x{0:x4} = 0x{1:x4})", v.Item1, v.Item2);
                }

                triggerListBox.Items.Add(triggerString);
            }
        }

        private void LiveSplitSession_StateChanged(object sender, EventArgs e)
        {
            BeginInvoke(new InvokeDelegate(UpdateConnectionState));
        }

        private void UpdateConnectionState()
        {
            if (m_liveSplitConnectionSession != null)
            {
                switch (m_liveSplitConnectionSession.State)
                {
                    case LiveSplitConnectionSessionState.NotConnected:
                        connectionStatusLabel.Text = "Not connected";
                        break;
                    case LiveSplitConnectionSessionState.Connecting:
                        connectionStatusLabel.Text = "Connecting";
                        break;
                    case LiveSplitConnectionSessionState.Connected:
                        connectionStatusLabel.Text = "Connected";
                        break;
                    case LiveSplitConnectionSessionState.Disconnecting:
                        connectionStatusLabel.Text = "Connected";
                        break;
                    case LiveSplitConnectionSessionState.Complete:
                        if (m_liveSplitConnectionSession.Error != null)
                        {
                            connectionStatusLabel.Text = "Error: " + m_liveSplitConnectionSession.Error.Message;
                        }
                        else
                        {
                            connectionStatusLabel.Text = "Not connected";
                        }
                        break;
                }
            }
            else
            {
                connectionStatusLabel.Text = "Not connected";
            }

            UpdateConnectButtonState();
        }

        private void UpdateConnectButtonState()
        {
            System.Net.IPAddress addr;
            int port;
            if (!System.Net.IPAddress.TryParse(ipTextBox.Text, out addr) || !int.TryParse(portTextBox.Text, out port))
            {
                connectButton.Enabled = false;
            }
            else
            {
                if (m_liveSplitConnectionSession != null)
                {
                    switch (m_liveSplitConnectionSession.State)
                    {
                        case LiveSplitConnectionSessionState.NotConnected:
                        case LiveSplitConnectionSessionState.Complete:
                            connectButton.Enabled = true;
                            connectButton.Text = "Connect";
                            break;
                        case LiveSplitConnectionSessionState.Connecting:
                            connectButton.Enabled = false;
                            connectButton.Text = "Connect";
                            break;
                        case LiveSplitConnectionSessionState.Connected:
                            connectButton.Enabled = false;
                            connectButton.Text = "Disconnect";
                            break;
                        case LiveSplitConnectionSessionState.Disconnecting:
                            connectButton.Enabled = false;
                            connectButton.Text = "Disconnect";
                            break;
                    }
                }
                else
                {
                    connectButton.Enabled = true;
                    connectButton.Text = "Connect";
                }
            }
        }

        private SplitProfile m_splitProfile;
        private LiveSplitConnectionSession m_liveSplitConnectionSession;
        private TimerSession m_timerSession;
    }
}
