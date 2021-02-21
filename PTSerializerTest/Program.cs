using System;
using System.IO;
using ProTrackerTools;

namespace PTSerializerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var mod = Serializer.DeSerializeMod("C:\\Users\\ianf\\Google Drive\\Amiga\\Mods\\Hoffman\\freerunner.mod");
            var pmod = P61Convert.Convert(mod);
            File.WriteAllBytes(@"C:\MyProjects\generator\Generator_asm\tunedata\samples\p61.myversion3", P61Convert.Serialize(pmod));
        }
    }
}
