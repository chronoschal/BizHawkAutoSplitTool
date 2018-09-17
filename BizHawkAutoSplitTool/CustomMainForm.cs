using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BizHawk.Client.Common;
using BizHawk.Emulation.Common;

namespace BizHawk.Client.EmuHawk
{
    class MemoryTrigger
    {
        public MemoryTrigger(uint triggerAddress, List<Tuple<uint, uint>> expectedMemoryValues)
        {
            TriggerAddress = triggerAddress;
            ExpectedMemoryValues = expectedMemoryValues;
        }

        public uint TriggerAddress { get; private set; }
        public IReadOnlyList<Tuple<uint, uint>> ExpectedMemoryValues { get; private set; }
    }

    public partial class CustomMainForm : Form, IExternalToolForm
    {
        public CustomMainForm()
        {
            InitializeComponent();
        }

        [RequiredService]
        private IMemoryDomains MemoryDomains { get; set; }

        [OptionalService]
        private IDebuggable Debuggable { get; set; }

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
            OnMemoryWrite();
        }

        /// <summary>
        /// This method is called instead of regular <see cref="UpdateValues"/>
        /// when emulator is runnig in turbo mode
        /// </summary>
        public void FastUpdate()
        {
            OnMemoryWrite();
        }

        public void NewUpdate(ToolFormUpdateType type)
        { }

        /// <summary>
        /// Restart is called the first time you call the form
        /// but also when you start playing a movie
        /// </summary>
        public void Restart()
        {

            //Debuggable.MemoryCallbacks.Add(new MemoryCallback(MemoryDomains.MainMemory.Name, MemoryCallbackType.Write, "", OnMemoryWrite, 0x079c, null));
        }

        private delegate void InvokeDelegate();

        private void connectButton_Click(object sender, EventArgs e)
        {
            if ((m_currentSession == null) || (m_currentSession.State == BizHawkAutoSplitTool.LiveSplitConnectionSessionState.Complete))
            {
                m_currentSession = new BizHawkAutoSplitTool.LiveSplitConnectionSession(ipTextBox.Text, int.Parse(portTextBox.Text));
                m_currentSession.StateChanged += LiveSplitSession_StateChanged;
                m_currentSession.Connect();
            }
            else if (m_currentSession.State == BizHawkAutoSplitTool.LiveSplitConnectionSessionState.Connected)
            {
                m_currentSession.Disconnect();
            }
            UpdateConnectionState();
        }

        private void ipTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void portTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void LiveSplitSession_StateChanged(object sender, EventArgs e)
        {
            BeginInvoke(new InvokeDelegate(UpdateConnectionState));
        }

        private void UpdateConnectionState()
        {
            if (m_currentSession != null)
            {
                switch (m_currentSession.State)
                {
                    case BizHawkAutoSplitTool.LiveSplitConnectionSessionState.NotConnected:
                        connectButton.Enabled = true;
                        connectButton.Text = "Connect";
                        connectionStatusLabel.Text = "Not connected";
                        break;
                    case BizHawkAutoSplitTool.LiveSplitConnectionSessionState.Connecting:
                        connectButton.Enabled = false;
                        connectButton.Text = "Connect";
                        connectionStatusLabel.Text = "Connecting";
                        break;
                    case BizHawkAutoSplitTool.LiveSplitConnectionSessionState.Connected:
                        connectButton.Enabled = true;
                        connectButton.Text = "Disconnect";
                        connectionStatusLabel.Text = "Connected";
                        break;
                    case BizHawkAutoSplitTool.LiveSplitConnectionSessionState.Disconnecting:
                        connectButton.Enabled = false;
                        connectButton.Text = "Disconnect";
                        connectionStatusLabel.Text = "Connected";
                        break;
                    case BizHawkAutoSplitTool.LiveSplitConnectionSessionState.Complete:
                        connectButton.Enabled = true;
                        connectButton.Text = "Connect";
                        if (m_currentSession.Error != null)
                        {
                            connectionStatusLabel.Text = "Error: " + m_currentSession.Error.Message;
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
                connectButton.Enabled = true;
                connectionStatusLabel.Text = "Not connected";
            }
        }

        private void OnMemoryWrite()
        {
            if (m_nextSplitConditionIndex < m_splitConditionList.Count)
            {
                var currentSplitCondition = m_splitConditionList[m_nextSplitConditionIndex];
                var currentWatchLocationValue = MemoryDomains.MainMemory.PeekByte(currentSplitCondition.TriggerAddress);
                if (currentWatchLocationValue != m_lastWatchLocationValue)
                {
                    bool wereConditionsMet = true;
                    foreach (var v in currentSplitCondition.ExpectedMemoryValues)
                    {
                        if (MemoryDomains.MainMemory.PeekByte(v.Item1) != v.Item2)
                        {
                            wereConditionsMet = false;
                            break;
                        }
                    }

                    if (wereConditionsMet)
                    {
                        ++m_nextSplitConditionIndex;
                        m_lastWatchLocationValue = -1;

                        if (m_currentSession != null)
                        {
                            m_currentSession.SendStartOrSplitTimerCommand();
                        }
                    }

                    m_lastWatchLocationValue = currentWatchLocationValue;
                }
            }
        }

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
        private List<MemoryTrigger> m_splitConditionList = new List<MemoryTrigger>
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
        };
        private int m_nextSplitConditionIndex = 0;
        private int m_lastWatchLocationValue = -1;
        private BizHawkAutoSplitTool.LiveSplitConnectionSession m_currentSession;
    }
}
