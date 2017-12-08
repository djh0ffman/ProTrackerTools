
using System;
using System.IO;

namespace PTSerializerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var ser = new PTSerializer.PTSerializer();
            string fileName = "C:\\Users\\ian.ford\\Google Drive\\Amiga\\Mods\\mod.miles per pattern";
            var original = File.ReadAllBytes(fileName);
            var mod = ser.DeSerializeMod(fileName);
            var data = ser.SerializeMod(mod);
            if (original.Length != data.Length) throw new Exception("size is different");
            for (var i = 0; i < original.Length; i++)
            {
                if (original[i] != data[i]) Console.WriteLine("Diff at {2} A: {0} B: {1}", original[i], data[i], i);
            }
        }
    }
}
