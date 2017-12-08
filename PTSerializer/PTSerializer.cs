using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSerializer
{
    public class PTSerializer
    {
        public Module DeSerializeMod(string filename)
        {
            var data = File.ReadAllBytes(filename);
            var reader = new BigEndianReader(new MemoryStream(data));
            return DeSerializeMod(reader);
        }

        public Module DeSerializeMod(BigEndianReader reader)
        {
            var mod = new Module();

            mod.Name = reader.ReadAscii(20);

            for (var i = 0; i < Module.MaxSamples; i++)
            {
                var sample = new Sample();

                sample.Name = reader.ReadAscii(22);
                sample.Length = reader.ReadInt16()*2;
                sample.FineTune = reader.ReadSByte();
                sample.Volume = reader.ReadByte();
                sample.RepeatStart = reader.ReadInt16()*2;
                sample.RepeatLength = reader.ReadInt16()*2;
                
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

        private Pattern DeSerializePattern(BinaryReader reader)
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

        private PatternItem DeSerializePatternItem(byte[] data)
        {
            var item = new PatternItem();
            item.Command = (CommandType) (int)(data[2] & 0x0f);
            item.CommandValue = data[3];
            item.Period = ((data[0] & 0x0f) << 8) + data[1];
            item.SampleNumber = (data[0] & 0xF0) + ((data[2] & 0xf0) >> 4);
            return (item);
        }

        public byte[] SerializeMod(Module mod)
        {
            var writer = new BigEndianWriter(new MemoryStream());

            writer.WriteAscii(mod.Name, 20);

            foreach (var sample in mod.Samples)
            {
                writer.WriteAscii(sample.Name, 22);
                writer.WriteInt16(sample.Length/2);
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

            for (var p = 0; p <= maxPatterns; p++)
            {
                writer.Write(SerializePattern(mod.Patterns[p]));
            }

            foreach (var sample in mod.Samples)
            {
                writer.Write(sample.Data);
            }
            
            return ((MemoryStream)writer.BaseStream).ToArray();
        }

        private byte[] SerializePattern(Pattern pattern)
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

        private byte[] SerializePatternItem(PatternItem item)
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
