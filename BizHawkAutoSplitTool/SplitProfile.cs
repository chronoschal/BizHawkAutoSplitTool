using System.Collections.Generic;

namespace BizHawkAutoSplitTool
{
    class SplitProfile
    {
        public SplitProfile(string name, IReadOnlyList<MemoryTrigger> splitTriggerList)
        {
            Name = name;
            SplitTriggerList = splitTriggerList;
        }

        public string Name { get; private set; }

        public IReadOnlyList<MemoryTrigger> SplitTriggerList { get; private set; }
    }
}
