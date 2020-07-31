
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace ProTrackerTools
{
    public class BigEndianWriter : BinaryWriter
    {
        public BigEndianWriter(Stream stream) : base(stream) { }
        
        public void WriteAscii(string text, int length)
        {
            var output = new byte[length];
            var convertedText = Encoding.ASCII.GetBytes(text);
            for (var i = 0; i < length; i++)
            {
                if (i < convertedText.Length)
                    output[i] = convertedText[i];
                else
                    output[i] = 0;
            }
            
            this.Write(output);
        }

        public void WriteInt16(int value)
        {
            var data = BitConverter.GetBytes(value);
            Array.Reverse(data);
            Write(data[2]);
            Write(data[3]);
        }      
        
        public byte[] ToArray()
        {
            return ((MemoryStream)BaseStream).ToArray();
        }

        public int Position
        {
            get { return (int)BaseStream.Position; }            
        }
    }
}
