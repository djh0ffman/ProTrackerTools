using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProTrackerTools;

namespace PTSerializerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            FixSLL();

            var mod = Serializer.DeSerializeMod("C:\\Users\\h0ffman\\My Drive\\Amiga\\Mods\\mod.footprints");
            mod.FullOptimise();

            Serializer.TestSomething(mod);

            var header = Serializer.SerializeTransposedSong(mod);
            var songHeader = Serializer.SerializePatternData(mod);
            var samples = Serializer.SerializeSampleData(mod);

            File.WriteAllBytes("C:\\temp\\test.reorder", header);
            File.WriteAllBytes("C:\\temp\\test.sng", songHeader);
            File.WriteAllBytes("C:\\temp\\test.smp", samples);

        }

        static void FixSLL()
        {
            var mod = Serializer.DeSerializeMod("C:\\temp\\high byte.mod");
            foreach (var pat in mod.Patterns)
            {
                foreach (var chan in pat.Channels)
                {
                    foreach (var item in chan.PatternItems)
                    {
                        if (item.Command == CommandType.PatternBreak)
                        {
                            item.Command = CommandType.ArpNone;
                            if (item.CommandValue != 0)
                            {
                                item.CommandValue = 0;
                            }

                        }
                    }

                }
            }
            var modBin = Serializer.SerializeMod(mod);
            File.WriteAllBytes("C:\\temp\\high_byte_fixed.mod", modBin);
        }
    }
}
