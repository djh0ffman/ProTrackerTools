using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Xml.Schema;

namespace ProTrackerTools
{
    public static class P61Convert
    {
        private const int FlagCompress = 0b10000000;

        private const int FlagCommandOnly = 0b01100000;
        private const int FlagCommandOnlyMask = 0b01110000;

        private const int FlagNoteOnly = 0b01110000;
        private const int FlagNoteOnlyMask = 0b01111000;

        private const int FlagAllMask = 0b01100000;

        private const int FlagEmptyRow = 0b01111111;

        private const int FlagDummy = 0b11111111;

        private const int CompressMask = 0b11000000;
        private const int CompressEmptyRows = 0b00000000;
        private const int CompressSameRows = 0b10000000;
        private const int CompressJump8 = 0b01000000;
        private const int CompressJump16 = 0b11000000;

        public static P61Module Convert(Module mod)
        {
            // essential part of the process
            // as P61 does not allow samples to have excess after their loops
            mod.OptimseLoopedSamples();
            mod.RemoveUnusedSamples();
            mod.RemoveDuplicatePatterns();
            mod.RemoveUnusedPatterns();

            var pmod = new P61Module();

            // song positions
            for (var i = 0; i < mod.SongLength; i++)
            {
                pmod.SongPositions.Add(mod.SongPositions[i]);
            }

            // samples 
            pmod.Samples = mod.Samples.ToList();

            // part 1 - create pattern data
            foreach (var pattern in mod.Patterns)
            {
                pmod.Patterns.Add(ConvertPattern(pattern));
            }

            // part 2 - compress pattern data
            foreach (var ppat in pmod.Patterns)
            {
                PackPattern(ppat);
            }

            return pmod;
        }

        private static void PackPattern(P61Pattern ppat)
        {
            for (var chanId = 0; chanId < 4; chanId++)
            {
                var rows = new List<byte[]>();

                // get all rows to an array
                using (var reader = new BigEndianReader(new MemoryStream(ppat.Channels[chanId])))
                {
                    while (!reader.EndOfStream())
                    {
                        rows.Add(GetRow(reader));
                    }
                }

                // pack the channel
                rows = RemoveEmptyRows(rows);
                rows = DedupeRows(rows);
                //rows = LoopRows(rows);

                // store the result
                var result = new List<byte>();
                foreach (var row in rows)
                {
                    result.AddRange(row);
                }

                ppat.Channels[chanId] = result.ToArray();
            }
        }
        
        private static List<byte[]> LoopRows(List<byte[]> rows)
        {
            var newRows = new List<byte[]>();

            for (var pos = 0; pos < rows.Count; pos++)
            {
                var searchRow = rows[pos];
                var foundRow = FindSameRow(searchRow, pos + 1, rows);
                if (foundRow > 0)
                {
                    // found the same row, check all rows between
                    var distance = foundRow - pos;
                    bool match = true;

                    for (var i = 0; i < distance; i++)
                    {
                        if (foundRow + i >= rows.Count || !CompareRow(rows[pos+i],rows[foundRow+i]))
                        {
                            match = false;
                            newRows.Add(rows[pos]);
                            break;
                        }
                    }

                    if (match)
                    {
                        // rows match, add loop
                        var byteCount = 0;  // start at 2 to include dummy and compress flag

                        for (var i = 0; i < distance; i++)
                        {
                            byteCount += rows[pos + i].Length;
                            newRows.Add(rows[pos + i]);
                        }

                        newRows.Add(new byte[] { FlagDummy });
                        
                        if (byteCount > 0xff)
                        {
                            newRows.Add(new byte[] { (byte)(distance - 1 | CompressJump16) });
                            newRows.Add(GetByte16(byteCount + 4));
                        }
                        else
                        {
                            newRows.Add(new byte[] { (byte)(distance - 1 | CompressJump8) });
                            newRows.Add(new byte[] { (byte)(byteCount + 3) });
                        }

                        pos = foundRow + distance - 1;
                    }
                }
                else
                {
                    newRows.Add(rows[pos]);
                }
            }

            return newRows;
        }

        private static int FindSameRow(byte[] searchRow, int startIndex, List<byte[]> inRows)
        {
            for (var i = startIndex; i < inRows.Count; i++)
            {
                if (CompareRow(searchRow, inRows[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        private static List<byte[]> RemoveEmptyRows(List<byte[]> rows)
        {
            var newRows = new List<byte[]>();

            for (var pos = 0; pos < rows.Count; pos++)
            {
                if (rows.Count < pos + 1 && rows[pos][0] == FlagEmptyRow && rows[pos + 1][0] == FlagEmptyRow)
                {
                    newRows.Add(new byte[] { (FlagEmptyRow | FlagCompress) });
                    var counter = 0;
                    while (pos < rows.Count - 1 && rows[pos + 1][0] == FlagEmptyRow)
                    {
                        counter++;
                        pos++;
                    }
                    newRows.Add(new byte[] { (byte)(counter | CompressEmptyRows) });
                }
                else
                {
                    newRows.Add(rows[pos]);
                }
            }

            return newRows;
        }

        private static List<byte[]> DedupeRows(List<byte[]> rows)
        {
            var newRows = new List<byte[]>();

            for (var pos = 0; pos < rows.Count; pos++)
            {
                if (pos == rows.Count - 1)
                {
                    // last pos
                    newRows.Add(rows[pos]);
                    continue;
                }

                if (!CompareRow(rows[pos], rows[pos + 1]))
                {
                    newRows.Add(rows[pos]);
                }
                else
                {
                    rows[pos][0] |= FlagCompress;
                    var counter = 1;
                    newRows.Add(rows[pos]);
                    pos++;

                    while (pos < rows.Count - 1 && CompareRow(rows[pos], rows[pos + 1]))
                    {
                        counter++;
                        pos++;
                    }
                    newRows.Add(new byte[] { (byte)(counter | CompressSameRows) });
                }
            }

            return newRows;
        }

        private static bool CompareRow(byte[] rowA, byte[] rowB)
        {
            if (rowA == null)
                return false;

            if (rowB == null)
                return false;

            if (rowA.Length != rowB.Length)
                return false;

            for (var i = 0; i < rowA.Length; i++)
            {
                if (rowA[i] != rowB[i])
                    return false;
            }

            return true;
        }

        private static byte[] GetRow(BigEndianReader reader)
        {
            var row = new List<byte>();
            var value = reader.ReadByte();
            row.Add(value);
            var rowSize = RowSize(value) - 1;
            for (var i = 0; i < rowSize; i++)
            {
                row.Add(reader.ReadByte());
            }
            return row.ToArray();
        }

        private static int RowSize(byte value)
        {
            if ((value & FlagCommandOnlyMask) == FlagCommandOnly)
            {
                // command only
                return 2;
            }
            else if ((value & FlagNoteOnlyMask) == FlagNoteOnly)
            {
                // note only
                return 2;
            }
            else if ((value & FlagAllMask) != FlagAllMask)
            {
                // full row
                return 3;
            }
            else
            {
                // empty or compressed
                return 1;
            }
        }

        private static P61Pattern ConvertPattern(Pattern pat)
        {
            var ppat = new P61Pattern();

            var resultA = new List<byte>();
            var resultB = new List<byte>();
            var resultC = new List<byte>();
            var resultD = new List<byte>();

            bool breakFlag = false;

            for (var lineId = 0; lineId < 64; lineId++)
            {
                var chanA = pat.Channels[0].PatternItems[lineId];
                var chanB = pat.Channels[1].PatternItems[lineId];
                var chanC = pat.Channels[2].PatternItems[lineId];
                var chanD = pat.Channels[3].PatternItems[lineId];

                resultA.AddRange(ConvertRow(chanA, ref breakFlag, lineId));
                resultB.AddRange(ConvertRow(chanB, ref breakFlag, lineId));
                resultC.AddRange(ConvertRow(chanC, ref breakFlag, lineId));
                resultD.AddRange(ConvertRow(chanD, ref breakFlag, lineId));

                if (breakFlag)
                {
                    break;
                }
            }

            ppat.Channels.Add(resultA.ToArray());
            ppat.Channels.Add(resultB.ToArray());
            ppat.Channels.Add(resultC.ToArray());
            ppat.Channels.Add(resultD.ToArray());

            return ppat;
        }

        private static byte[] ConvertRow(PatternItem item, ref bool breakFlag, int lineId)
        {
            int note = PeriodToNote(item.Period);
            var cmd = ConvertCommand(item.Command, item.CommandValue, ref breakFlag, lineId);

            int temp;

            bool hasNote = item.SampleNumber > 0 | note > 0;
            bool hasCmd = cmd.Command > 0 | cmd.Value > 0;

            if (hasNote && hasCmd)
            {
                // onnnnnni iiiicccc bbbbbbbb    Note, instrument and command
                temp = (note << 17) + (item.SampleNumber << 12) + (cmd.Command << 8) + cmd.Value;
                return GetByte24(temp);
            }
            else if (!hasNote && hasCmd)
            {
                // * o110cccc bbbbbbbb		Only command
                temp = (FlagCommandOnly << 8) + (cmd.Command << 8) + cmd.Value;
                return GetByte16(temp);

            }
            else if (hasNote && !hasCmd)
            {
                // o1110nnn nnniiiii		Note and instrument
                temp = (FlagNoteOnly << 8) + (note << 5) + item.SampleNumber;
                return GetByte16(temp);
            }
            else
            {
                // * o1111111			Empty note
                return new byte[] { FlagEmptyRow };
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
                // TODO: option to sign
                //writer.WriteAscii("P61A", 4);

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

                // TODO: Option separate file or no samples
                foreach (var sample in pmod.Samples)
                {
                    writer.Write(sample.Data);
                }

                modData = writer.ToArray();
            }

            return modData;
        }

        // p61 command optimizer
        private static P61Command ConvertCommand(CommandType command, int value, ref bool breakFlag, int lineId)
        {
            int newCmd = (int)command;

            switch (command)
            {
                case CommandType.ArpNone:
                    if (value > 0)
                        newCmd = 0x08;
                    break;
                case CommandType.SlideUp:
                case CommandType.SlideDown:
                    if (value == 0)
                        newCmd = 0;
                    break;
                case CommandType.UnusedSync:
                    newCmd = 0x0e;
                    value = (value & 0x0f) + 0x80;
                    break;
                case CommandType.TonePortamentoVolumeSlide:
                    value = ConvertVolumeSlide(value);
                    if (value == 0)
                        newCmd = 3;
                    break;
                case CommandType.VibratoVolumeSlide:
                    value = ConvertVolumeSlide(value);
                    if (value == 0)
                        newCmd = 4;
                    break;
                case CommandType.VolumeSlide:
                    value = ConvertVolumeSlide(value);
                    break;
                case CommandType.PositionJump:
                    // set break on line and store command
                    breakFlag = true;
                    break;
                case CommandType.SetVolume:
                    if (value > 64)
                        value = 64;
                    break;
                case CommandType.PatternBreak:
                    // check position, if last line then skip
                    // check for break and skip if needed
                    value = 0;
                    if (breakFlag || lineId == 63)
                        newCmd = 0;
                    else
                        breakFlag = true;
                    break;
                case CommandType.Extended:
                    switch ((value & 0xf0) >> 4)
                    {
                        case 0:
                            // filter value and
                            value &= 0xf1;
                            break;
                        case 1:
                        case 2:
                        case 9:
                        case 0x0a:
                        case 0x0b:
                        case 0x0d:
                        case 0x0e:
                            // clear 0 value command
                            if ((value & 0x0f) == 0)
                            {
                                newCmd = 0;
                                value = 0;
                            }
                            break;
                    }
                    break;
            }

            return new P61Command()
            {
                Command = newCmd,
                Value = value
            };
        }

        private static int ConvertVolumeSlide(int value)
        {
            if ((value & 0xf0) == 0)
                return value & 0x0f;
            else
                return (-((value & 0xf0) >> 4)) & 0xff;
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
                default: throw new Exception("note value invalid");
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
