using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProTrackerTools
{
    public static class P61Convert
    {
        public static P61Module Convert(Module mod)
        {
            // essential part of the process
            // as P61 does not allow samples to be to have excess after their loops
            mod.OptimseLoopedSamples();

            var pmod = new P61Module();

            // song positions
            for (var i = 0; i < mod.SongLength; i++)
            {
                pmod.SongPositions.Add(mod.SongPositions[i]);
            }

            // samples 
            pmod.Samples = mod.Samples.ToList();

            foreach (var pattern in mod.Patterns)
            {
                pmod.Patterns.Add(ConvertPattern(pattern));
            }

            return pmod;
        }

        private static P61Pattern ConvertPattern(Pattern pat)
        {
            var ppat = new P61Pattern();

            for (var i = 0; i < 4; i++)
            {
                ppat.Channels.Add(ConvertChannel(pat.Channels[i]));
            }

            return ppat;
        }

        private static byte[] ConvertChannel(Channel chan)
        {
            using (var writer = new BigEndianWriter(new MemoryStream()))
            {
                foreach (var line in chan.PatternItems)
                {
                    writer.Write(ConvertLine(line));
                }

                return writer.ToArray();
            }
        }

        private static byte[] ConvertLine(PatternItem item)
        {
            int note = PeriodToNote(item.Period);
            int cmd = (int)item.Command;
            int temp;

            // volume ceiling
            var cmdValue = (int)item.CommandValue;
            if (item.Command == CommandType.SetVolume && cmdValue > 64)
                cmdValue = 64;

            if (note > 0 && (cmd > 0 || cmdValue > 0))
            {
                // onnnnnni iiiicccc bbbbbbbb    Note, instrument and command
                int noteCmd = (note << 9) + (item.SampleNumber << 4) + (int)item.Command;
                temp = (note << 17) + (item.SampleNumber << 12) + (cmd << 8) + cmdValue;
                return GetByte24(temp);
            }
            else if (note == 0 && (cmd > 0 || cmdValue > 0))
            {
                // * o110cccc bbbbbbbb		Only command
                temp = (0b01110000<<8) + (cmd << 8) + cmdValue;
                return GetByte16(temp);
                
            }
            else if (note > 0 && cmd == 0 && cmdValue == 0)
            {
                // o1110nnn nnniiiii		Note and instrument
                temp = (0b01110000<<8) + (note << 5) + item.SampleNumber;
                return GetByte16(temp);
            }
            else
            {
                // * o1111111			Empty note
                return new byte[]{ 0b01111111 };
            }
        }

        public static byte[] Serialize(P61Module pmod)
        {
            // write out samples
            byte[] samples;
            using (var writer = new BigEndianWriter(new MemoryStream()))
            {
                // TODO: sample optimisation
                writer.Write((byte)pmod.Patterns.Count);
                writer.Write((byte)pmod.Samples.Count);     

                foreach (var sample in pmod.Samples)
                {
                    writer.WriteInt16(sample.Length < 1 ? 1 : sample.Length / 2);
                    writer.Write((byte)sample.FineTune);    // TODO: check if pack flags go here
                    writer.Write((byte)sample.Volume);
                    if (sample.RepeatStart == 0 && sample.RepeatLength == 2)
                    {
                        // one-shot sample
                        writer.WriteInt16(-1);
                    }
                    else
                    {
                        writer.WriteInt16(sample.RepeatStart / 2);
                    }
                }

                samples = writer.ToArray();
            }
               
            byte[] patternData;

            var chanIndex = new int[pmod.Patterns.Count, 4];

            using (var writer = new BigEndianWriter(new MemoryStream()))
            {
                for (var i = 0; i < 4; i++)
                {
                    var patId = 0;
                    foreach (var pat in pmod.Patterns)
                    {
                        chanIndex[patId, i] = writer.Position;
                        writer.Write(pat.Channels[i].ToArray());
                        patId++;
                    }
                }

                patternData = writer.ToArray();
            }

            using (var writer = new BigEndianWriter(new MemoryStream()))
            {
                //  channel data pointers
                for (var i = 0; i < pmod.Patterns.Count; i++)
                {
                    writer.WriteInt16(chanIndex[i, 0]);
                    writer.WriteInt16(chanIndex[i, 1]);
                    writer.WriteInt16(chanIndex[i, 2]);
                    writer.WriteInt16(chanIndex[i, 3]);
                }

                // pattern positions
                foreach (var patId in pmod.SongPositions)
                {
                    writer.Write((byte)patId);
                }
                writer.Write((byte)0xff);  // end of song positions

                writer.Write(patternData); // append the pattern data

                patternData = writer.ToArray();  // lump it together
            }

            byte[] modData;

            using (var writer = new BigEndianWriter(new MemoryStream()))
            {
                // sample start position
                var sampleStart = samples.Length + patternData.Length + 2;
                bool oddPatternData = false;
                if (sampleStart % 2 == 1)
                {
                    sampleStart++;
                    oddPatternData = true;
                }

                writer.WriteInt16(samples.Length + patternData.Length + 2);
                writer.Write(samples);
                writer.Write(patternData);

                if (oddPatternData)     // keep samples aligned to word
                {
                    writer.Write((byte)0);
                }

                // TODO: Option to not export
                foreach (var sample in pmod.Samples)
                {
                    writer.Write(sample.Data);
                }

                modData = writer.ToArray();
            }

            return modData;
        }

        private static byte PeriodToNote(int period)
        {
            switch (period)
            {
                case 0: return 0;
                case 856: return 1;
                case 808: return 2;
                case 762: return 3;
                case 720: return 4;
                case 678: return 5;
                case 640: return 6;
                case 604: return 7;
                case 570: return 8;
                case 538: return 9;
                case 508: return 10;
                case 480: return 11;
                case 453: return 12;
                case 428: return 13;
                case 404: return 14;
                case 381: return 15;
                case 360: return 16;
                case 339: return 17;
                case 320: return 18;
                case 302: return 19;
                case 285: return 20;
                case 269: return 21;
                case 254: return 22;
                case 240: return 23;
                case 226: return 24;
                case 214: return 25;
                case 202: return 26;
                case 190: return 27;
                case 180: return 28;
                case 170: return 29;
                case 160: return 30;
                case 151: return 31;
                case 143: return 32;
                case 135: return 33;
                case 127: return 34;
                case 120: return 35;
                case 113: return 36;
                default: throw new Exception("note id invalid");
            }
        }

        private static byte[] GetByte16(int value)
        {
            var data = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                data.Reverse();
            }
            return new byte[] { data[1], data[0] };
        }

        private static byte[] GetByte24(int value)
        {
            var data = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                data.Reverse();
            }
            return new byte[] { data[2], data[1], data[0] };
        }

    }
}
