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
            // Console.WriteLine($"test ADO.NET   - {PiluleDal.CheckVersionAdoNet()}");
            // Console.WriteLine($"test DapperORM - {PiluleDal.CheckVersionDaperOrm()}");

            /*var debugValue = PiluleDal.GetDebugValue("GoodsDictionary");
            if (debugValue != null)
            {
                foreach (var items in debugValue) //foreach (ICollection<KeyValuePair<string, object>> items in debugValue)
                {
                    foreach (var item in items)
                    {
                        Console.Write($"{item.Key}={item.Value} ");
                    }
                    Console.WriteLine();
                }
            }*/

            // var result = PiluleDal.ExecuteNonSelectCommand();

            /*Console.WriteLine(PiluleDal.DropDataBase());
            Console.WriteLine(PiluleDal.ExecuteScript("CreateDB"));
            Console.WriteLine(PiluleDal.ExecuteScript("CreateTable_Version"));
            Console.WriteLine(PiluleDal.ExecuteScript("CreateTable_GoodsDictionary"));
            Console.WriteLine(PiluleDal.ExecuteScript("CreateTable_StockBalance"));
            Console.WriteLine(PiluleDal.InsertDataExecute("DupelData"));

            Console.WriteLine(
                PiluleDal.InsertDataExecuteBulk(
                    new List<PiluleDal.GoodsDictionary>
                        {
                            new PiluleDal.GoodsDictionary { Name = "xxx", Price = 25, Comment = "nop" }
                        }));*/


            int cnt;
            var goodsDictionary = PiluleDal.ExecuteStorageProc(out cnt);
            goodsDictionary?.ToList().ForEach(Console.WriteLine);
            Console.WriteLine(cnt);

            /*goodsDictionary = PiluleDal.GetGoodsDictionary(3);
            goodsDictionary?.ToList().ForEach(Console.WriteLine);*/

            //var goodsDictionary = PiluleDal.GetGoodsDictionary(new List<int> { 1, 3 });
            //goodsDictionary?.ToList().ForEach(Console.WriteLine); 

            //var tuple = PiluleDal.MultiSelect();
            //tuple?.Item1?.ToList().ForEach(Console.WriteLine);
            //tuple?.Item2?.ToList().ForEach(Console.WriteLine);

            //var result = PiluleDal.MultiMapping();
            //result?.ToList().ForEach(Console.WriteLine);

            /*var mmf = MemoryMappedFile.CreateNew("MyFileInFile", 1);
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
            }*/

            Console.ReadKey();
        }
    }
}
