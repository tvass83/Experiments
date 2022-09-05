using System;
using System.Threading;

namespace ConsoleApp1_StdErr
{
    class Program
    {
        static void Main(string[] args)
        {
            //Thread.Sleep(15000);

            for (int i = 0; i < 10000; i++)
            {
                Console.Error.WriteLine("e");
            }

            Environment.Exit(0);
        }
    }
}
