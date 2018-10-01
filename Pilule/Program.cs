// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="urb31075">
//  All REight Reserved 
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Pilule
{
    using System;

    /// <summary>
    /// The program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        private static void Main(string[] args)
        {
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
