using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BizHawkAutoSplitTool
{
    enum LiveSplitConnectionSessionState
    {
        NotConnected,
        Connecting,
        Connected,
        Disconnecting,
        Complete,
    }

    class LiveSplitConnectionSession : IDisposable
    {
        public LiveSplitConnectionSession(string ip, int port)
        {
            m_ip = ip;
            m_port = port;

            m_lock = new object();
            m_state = LiveSplitConnectionSessionState.NotConnected;
        }

        public LiveSplitConnectionSessionState State
        {
            get
            {
                lock (m_lock)
                {
                    return m_state;
                }
            }
        }

        public event EventHandler StateChanged;

        public Exception Error
        {
            get
            {
                lock (m_lock)
                {
                    return m_error;
                }
            }
        }

        public void Connect()
        {
            lock (m_lock)
            {
                if (m_state != LiveSplitConnectionSessionState.NotConnected)
                {
                    throw new InvalidOperationException("Connect() can only be called once.");
                }
                ChangeStateWithLock(LiveSplitConnectionSessionState.Connecting);

                Task.Run(new Action(RunConnectionSession));
            }
        }

        public void Disconnect()
        {
            SendCommand(Command.Disconnect);
        }

        public void SendResetTimerCommand()
        {
            SendCommand(Command.ResetTimer);
        }

        public void SendStartOrSplitTimerCommand()
        {
            SendCommand(Command.StartOrSplitTimer);
        }

        public void SendPauseTimerCommand()
        {
            SendCommand(Command.PauseTimer);
        }

        public void Dispose()
        {
            Disconnect();
        }

        private enum Command
        {
            Disconnect,
            ResetTimer,
            StartOrSplitTimer,
            PauseTimer,
        }

        private async void RunConnectionSession()
        {
            Exception error = null;
            try
            {
                var tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(m_ip, m_port);
                var connectionStream = tcpClient.GetStream();

                lock (m_lock)
                {
                    ChangeStateWithLock(LiveSplitConnectionSessionState.Connected);
                }

                var receiveBuffer = new byte[1];
                while (true)
                {
                    var readTask = connectionStream.ReadAsync(receiveBuffer, 0, 1);
                    var commandReadyWaitTask = m_commandReady.WaitAsync();
                    var completedTask = await Task.WhenAny(new Task[] { readTask, commandReadyWaitTask });
                    
                    if (completedTask == readTask)
                    {
                        var readByteCount = readTask.Result;
                        if (readByteCount == 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        List<Command> pendingCommandsList;
                        lock (m_lock)
                        {
                            pendingCommandsList = m_commandQueue;
                            m_commandQueue = new List<Command>();
                        }

                        bool shouldDisconnect = false;
                        foreach (var c in pendingCommandsList)
                        {
                            switch (c)
                            {
                                case Command.Disconnect:
                                    shouldDisconnect = true;
                                    break;
                                case Command.ResetTimer:
                                    await connectionStream.WriteAsync(ResetTimerLiveSplitMessage, 0, ResetTimerLiveSplitMessage.Length);
                                    break;
                                case Command.StartOrSplitTimer:
                                    await connectionStream.WriteAsync(StartOrSplitTimerLiveSplitMessage, 0, StartOrSplitTimerLiveSplitMessage.Length);
                                    break;
                                case Command.PauseTimer:
                                    await connectionStream.WriteAsync(PauseTimerLiveSplitMessage, 0, PauseTimerLiveSplitMessage.Length);
                                    break;
                            }

                            if (shouldDisconnect)
                            {
                                break;
                            }
                        }

                        if (shouldDisconnect)
                        {
                            break;
                        }
                    }
                }

                connectionStream.Close();
                tcpClient.Close();
            }
            catch (Exception e)
            {
                error = e;
            }

            lock (m_lock)
            {
                ChangeStateWithLock(LiveSplitConnectionSessionState.Complete);
                m_error = error;
            }
        }

        private void SendCommand(Command command)
        {
            lock (m_lock)
            {
                m_commandQueue.Add(command);
                m_commandReady.Release();
            }
        }

        private void ChangeStateWithLock(LiveSplitConnectionSessionState newState)
        {
            m_state = newState;

            Task.Run(() => StateChanged?.Invoke(this, EventArgs.Empty));
        }

        private string m_ip;
        private int m_port;
        private object m_lock;
        private LiveSplitConnectionSessionState m_state;
        private Exception m_error;
        private SemaphoreSlim m_commandReady = new SemaphoreSlim(0, 1);
        private List<Command> m_commandQueue = new List<Command>();

        private readonly byte[] StartOrSplitTimerLiveSplitMessage = Encoding.UTF8.GetBytes("startorsplit\r\n");
        private readonly byte[] ResetTimerLiveSplitMessage = Encoding.UTF8.GetBytes("reset\r\n");
        private readonly byte[] PauseTimerLiveSplitMessage = Encoding.UTF8.GetBytes("pause\r\n");
    }
}
