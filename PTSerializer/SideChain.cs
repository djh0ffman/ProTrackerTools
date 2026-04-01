using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProTrackerTools
{
    public class SideChain
    {
        public int DrumChannel { get; set; }
        public List<ChainTrigger> Triggers { get; set; }
        public List<ChainDuck> DuckSamples { get; set; }
    }

    public class ChainDuck
    {
        public int SampleNumber { get; set; }
        public float Reduction { get; set; }
        public int Lines { get; set; }
        public int Delay { get; set; }
        public List<int> VolumeValues { get; set; } = new List<int>();
        public int NewVolume { get; set; }
    }

    public class ChainTrigger
    {
        public int SampleNumber { get; set; }
        public int OffsetValue { get; set; }
    }
}
