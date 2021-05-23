using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProTrackerTools
{
    public class P61Module
    {
        public List<int> SongPositions { get; set; } = new List<int>();
        public List<P61Pattern> Patterns { get; set; } = new List<P61Pattern>();
        public List<Sample> Samples { get; set; } = new List<Sample>();
    }

    public class P61Pattern
    {
        public List<byte[]> Channels { get; set; } = new List<byte[]>();
    }
}
