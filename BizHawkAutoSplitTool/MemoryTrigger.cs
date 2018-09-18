using BizHawk.Emulation.Common;
using System;
using System.Collections.Generic;

namespace BizHawkAutoSplitTool
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

    class MemoryTriggerWatcher
    {
        public MemoryTriggerWatcher(MemoryTrigger memoryTrigger, IMemoryDomains memoryDomains, IDebuggable debuggable)
        {
            m_memoryTrigger = memoryTrigger;
            m_memoryDomains = memoryDomains;
            m_debuggable = debuggable;
        }

        public event EventHandler Triggered;

        public bool IsTriggered
        {
            get
            {
                lock (m_lock)
                {
                    return m_isTriggered;
                }
            }
        }

        public void Update()
        {
            var currentWatchLocationValue = m_memoryDomains.MainMemory.PeekByte(m_memoryTrigger.TriggerAddress);
            if (currentWatchLocationValue != m_lastWatchLocationValue)
            {
                bool wereConditionsMet = true;
                foreach (var v in m_memoryTrigger.ExpectedMemoryValues)
                {
                    if (m_memoryDomains.MainMemory.PeekByte(v.Item1) != v.Item2)
                    {
                        wereConditionsMet = false;
                        break;
                    }
                }

                if (wereConditionsMet)
                {
                    lock (m_lock)
                    {
                        m_isTriggered = true;
                    }
                    Triggered?.Invoke(this, EventArgs.Empty);
                }

                m_lastWatchLocationValue = currentWatchLocationValue;
            }
        }

        private MemoryTrigger m_memoryTrigger;
        private IMemoryDomains m_memoryDomains;
        private IDebuggable m_debuggable;

        private object m_lock = new object();
        private bool m_isTriggered = false;
        private int m_lastWatchLocationValue = -1;
    }
}
