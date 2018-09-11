using System;

namespace Pilule
{
    using System.Collections.Generic;
    using System.IO.MemoryMappedFiles;
    using System.Linq;

    using PiluleDAL;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine($"test ADO.NET   - {PiluleDal.CheckVersionAdoNet()}");
            Console.WriteLine($"test DapperORM - {PiluleDal.CheckVersionDaperOrm()}");
            var goodsDictionary = PiluleDal.GetGoodsDictionary();
            if (goodsDictionary != null)
            {
                foreach (var c in goodsDictionary)
                  {
                      Console.WriteLine(c);
                  }
            }

            var mmf = MemoryMappedFile.CreateNew("MyFileInFile", 1);
            using (var writer = mmf.CreateViewAccessor(0, 1))
            {
                byte msg = 25;
                writer.Write(0, msg);   
            }

            var mmf1 = MemoryMappedFile.OpenExisting("MyFileInFile");
            using (var reader = mmf1.CreateViewAccessor(0, 1, MemoryMappedFileAccess.Read))
            {
                var msg = reader.ReadByte(0);
                Console.WriteLine(msg);
            }

            Console.ReadKey();
        }
    }
}
