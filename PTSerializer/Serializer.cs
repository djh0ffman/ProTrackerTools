using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ProTrackerTools
{
    public static class Serializer
    {
        public static Module DeSerializeMod(byte[] modData)
        {
            using (var reader = new BigEndianReader(new MemoryStream(modData)))
            {
                return DeSerializeMod(reader);
            }
        }

        public static Module DeSerializeMod(string filename)
        {
            var data = File.ReadAllBytes(filename);
            var reader = new BigEndianReader(new MemoryStream(data));
            return DeSerializeMod(reader);
        }

        public static Module DeSerializeMod(BigEndianReader reader)
        {
            var mod = new Module();

            mod.Name = reader.ReadAscii(20);

            for (var i = 0; i < Module.MaxSamples; i++)
            {
                var sample = new Sample();

                sample.Name = reader.ReadAscii(22);
                sample.Length = reader.ReadUInt16() * 2;
                sample.FineTune = reader.ReadSByte();
                sample.Volume = reader.ReadByte();
                sample.RepeatStart = reader.ReadUInt16() * 2;
                sample.RepeatLength = reader.ReadUInt16() * 2;

                mod.Samples[i] = sample;
            }

            mod.SongLength = reader.ReadByte();
            mod.Unknown = reader.ReadByte();

            for (var i = 0; i < 128; i++)
            {
                mod.SongPositions[i] = reader.ReadByte();
            }

            var tag = reader.ReadAscii(4);
            if (tag != "M.K." && tag != "M!K!")
                throw new Exception("File is not a protracker module");

            var maxUsedPatterns = mod.SongPositions.Max();

            for (var i = 0; i <= maxUsedPatterns; i++)
            {
                mod.Patterns.Add(DeSerializePattern(reader));
            }

            foreach (var sample in mod.Samples)
            {
                sample.Data = reader.ReadBytes(sample.Length);
            }

            return (mod);
        }

        private static Pattern DeSerializePattern(BinaryReader reader)
        {
            var pattern = new Pattern();
            for (var line = 0; line < 64; line++)
            {
                for (var chan = 0; chan < 4; chan++)
                {
                    var item = DeSerializePatternItem(reader.ReadBytes(4));
                    pattern.Channels[chan].PatternItems[line] = item;
                }
            }
            return pattern;
        }

        private static PatternItem DeSerializePatternItem(byte[] data)
        {
            var item = new PatternItem();
            item.Command = (CommandType)(int)(data[2] & 0x0f);
            item.CommandValue = data[3];
            item.Period = ((data[0] & 0x0f) << 8) + data[1];
            item.SampleNumber = (data[0] & 0xF0) + ((data[2] & 0xf0) >> 4);
            return (item);
        }

        public static byte[] SerializeHeader(Module mod)
        {
            var writer = new BigEndianWriter(new MemoryStream());

            writer.WriteAscii(mod.Name, 20);

            foreach (var sample in mod.Samples)
            {
                writer.WriteAscii(sample.Name, 22);
                writer.WriteInt16(sample.Length / 2);
                writer.Write((sbyte)sample.FineTune);
                writer.Write((byte)sample.Volume);
                writer.WriteInt16(sample.RepeatStart / 2);
                writer.WriteInt16(sample.RepeatLength / 2);
            }

            writer.Write((byte)mod.SongLength);
            writer.Write(mod.Unknown);

            for (var i = 0; i < 128; i++)
            {
                writer.Write((byte)mod.SongPositions[i]);
            }

            var maxPatterns = mod.SongPositions.Max();

            if (maxPatterns > 63)
                writer.WriteAscii("M!K!", 4);
            else
                writer.WriteAscii("M.K.", 4);

            return writer.ToArray();
        }

        // serializes only the pattern data ( well and the header! dam naming)
        public static byte[] SerializePatternData(Module mod)
        {
            var writer = new BigEndianWriter(new MemoryStream());
            writer.Write(SerializeHeader(mod));

            writer.Write(SerializeAllPatterns(mod));

            return writer.ToArray();
        }

        public static byte[] SerializeAllPatterns(Module mod)
        {
            var writer = new BigEndianWriter(new MemoryStream());
            var maxPatterns = mod.SongPositions.Max();

            for (var p = 0; p <= maxPatterns; p++)
            {
                writer.Write(SerializePattern(mod.Patterns[p]));
            }

            return writer.ToArray();
        }

        
        public static byte[] SerializeTransposedSong(Module mod)
        {
            var writer = new BigEndianWriter(new MemoryStream());
            writer.Write(SerializeHeader(mod));

            TestSomething(mod);

            var patternData = SerializeAllPatterns(mod);
            patternData = TransposePatternData(patternData);
            writer.Write(patternData);

            return writer.ToArray();
        }

        public static void TestSomething(Module mod)
        {
            var patternData = SerializeAllPatterns(mod);

            var indexes = new List<int>();
            var values = new List<UInt32>();

            var reader = new BigEndianReader(new MemoryStream(patternData));

            while (!reader.EndOfStream())
            {
                var value = reader.ReadUInt32();
                if (!values.Contains(value))
                {
                    values.Add(value);
                }
                indexes.Add(values.IndexOf(value)); 
            }

            var writer = new BigEndianWriter(new MemoryStream());
            foreach (var index in indexes)
            {
                writer.WriteInt16(index);
            }
            foreach (var value in values)
            {
                writer.WriteUInt32(value);
            }

            var result = writer.ToArray();
        }

        public static byte[] TransposePatternData(byte[] patternData)
        {
            var chan1 = new BigEndianWriter(new MemoryStream());
            var chan2 = new BigEndianWriter(new MemoryStream());
            var chan3 = new BigEndianWriter(new MemoryStream());
            var chan4 = new BigEndianWriter(new MemoryStream());

            var reader = new BigEndianReader(new MemoryStream(patternData));

            for (var i = 0; i < patternData.Length / (4 * 4); i++)
            {
                chan1.Write(reader.ReadBytes(4));
                chan2.Write(reader.ReadBytes(4));
                chan3.Write(reader.ReadBytes(4));
                chan4.Write(reader.ReadBytes(4));
            }

            var final = new BigEndianWriter(new MemoryStream());
            final.Write(chan1.ToArray());
            final.Write(chan2.ToArray());
            final.Write(chan3.ToArray());
            final.Write(chan4.ToArray());

            return final.ToArray();

        }

        // serializes the samples
        public static byte[] SerializeSampleData(Module mod, bool doubleDelta = false)
        {
            var writer = new BigEndianWriter(new MemoryStream());

            foreach (var sample in mod.Samples)
            {
                if (sample.Length > 2)
                {
                    if (doubleDelta)
                    {
                        writer.Write(DoubleDelta(sample.Data));
                    }
                    else
                    {
                        writer.Write(sample.Data);
                    }
                }
            }

            return writer.ToArray();
        }

        public static byte[] DoubleDelta(byte[] data)
        {
            var delta = new byte[data.Length];
            int acm = 0;
            int temp = 0;
            for (int i = 0; i < data.Length; i++)
            {
                temp = (sbyte)data[i];
                temp /= 2;
                acm = temp - acm;
                delta[i] = (byte)(sbyte)acm;
                acm = temp;
            }

            return (delta);
        }

        public static byte[] SerializeMod(Module mod)
        {
            var writer = new BigEndianWriter(new MemoryStream());

            var patterns = SerializePatternData(mod);
            var samples = SerializeSampleData(mod);

            writer.Write(patterns);
            writer.Write(samples);

            return writer.ToArray();
        }

        public static byte[] SerializePattern(Pattern pattern)
        {
            var writer = new BigEndianWriter(new MemoryStream());
            for (var line = 0; line < 64; line++)
            {
                for (var chan = 0; chan < 4; chan++)
                {
                    writer.Write(SerializePatternItem(pattern.Channels[chan].PatternItems[line]));
                }
            }
            return ((MemoryStream)writer.BaseStream).ToArray();
        }

        private static byte[] SerializePatternItem(PatternItem item)
        {
            var output = new byte[4];

            output[0] = (byte) ((item.SampleNumber & 0xF0) + ((item.Period & 0x0F00) >> 8));
            output[1] = (byte) (item.Period & 0x00FF);
            output[2] = (byte) (item.Command + ((item.SampleNumber & 0x0F) << 4));
            output[3] = item.CommandValue;

            return output;
        }
    }
}
