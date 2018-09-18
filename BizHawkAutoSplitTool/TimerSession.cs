using BizHawk.Emulation.Common;
using System;

namespace BizHawkAutoSplitTool
{
    class TimerSession : IDisposable
    {
        public TimerSession(SplitProfile splitProfile, IMemoryDomains memoryDomains, IDebuggable debuggable)
        {
            m_splitProfile = splitProfile;
            m_memoryDomains = memoryDomains;
            m_debuggable = debuggable;
        }

        public int CurrentSplitIndex { get; private set; } = 0;

        public event EventHandler<EventArgs> Split;

        public void Dispose()
        {
        }

        public void OnFrame()
        {
            if (m_currentMemoryTriggerWatcher != null)
            {
                m_currentMemoryTriggerWatcher.Update();
            }

            //var splitTriggerList = m_splitProfile.SplitTriggerList;
            //if (m_currentSplitConditionIndex < splitTriggerList.Count)
            //{
            //    var currentSplitCondition = splitTriggerList[m_currentSplitConditionIndex];

            //    //if (wereConditionsMet)
            //    //{
            //    //    ++m_nextSplitConditionIndex;
            //    //    m_lastWatchLocationValue = -1;

            //    //    if (m_currentSession != null)
            //    //    {
            //    //        m_currentSession.SendStartOrSplitTimerCommand();
            //    //    }
            //    //}
            //}
        }

        private void UpdateMemoryTrigger()
        {
            if (m_currentMemoryTriggerWatcher != null)
            {
                m_currentMemoryTriggerWatcher.Triggered -= MemoryTriggerWatcher_Triggered;
            }

            if (CurrentSplitIndex < m_splitProfile.SplitTriggerList.Count)
            {
                m_currentMemoryTriggerWatcher = new MemoryTriggerWatcher(
                    m_splitProfile.SplitTriggerList[CurrentSplitIndex],
                    m_memoryDomains,
                    m_debuggable);

                m_currentMemoryTriggerWatcher.Triggered += MemoryTriggerWatcher_Triggered;
            }
            else
            {
                m_currentMemoryTriggerWatcher = null;
            }
        }

        private void MemoryTriggerWatcher_Triggered(object sender, System.EventArgs e)
        {
            Split?.Invoke(this, EventArgs.Empty);

            ++CurrentSplitIndex;
            UpdateMemoryTrigger();
        }

        private SplitProfile m_splitProfile;
        private IMemoryDomains m_memoryDomains;
        private IDebuggable m_debuggable;
        private MemoryTriggerWatcher m_currentMemoryTriggerWatcher;
    }
}
