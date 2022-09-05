using System;
using System.IO;

namespace ConsoleApp1_OpenFileForExclusiveAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            //string file = @"c:\Users\tamas\AppData\Local\Apps\2.0\863AVAL4.BGO\LYB0ZW2L.QK5\syne..tion_f3fdf5dc6daa082c_0000.0001_ac8f414cc8c7faca\Humanizer.dll";
            string file1 = @"c:\Users\tamas\AppData\Local\Apps\2.0\863AVAL4.BGO\LYB0ZW2L.QK5\syne...app_f3fdf5dc6daa082c_0008.0003_bbdde2bed936ea16\IdentityModel.dll";
            string file2 = @"c:\Users\tamas\AppData\Local\Apps\2.0\863AVAL4.BGO\LYB0ZW2L.QK5\syne..tion_f3fdf5dc6daa082c_0002.0000_cd02186652d014a9\IdentityModel.dll";

            using (var fs = File.Open(file1, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite | FileShare.Delete))
            {
                using (var fs2 = File.Open(file2, FileMode.Open, FileAccess.Read, FileShare.Read | FileShare.Delete))
                {

                }

                Console.WriteLine("Press <ENTER> to let go of the file...");
                Console.ReadLine();
            }
        }
    }
}
