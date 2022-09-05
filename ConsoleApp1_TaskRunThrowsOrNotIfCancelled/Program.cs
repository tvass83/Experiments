using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1_TaskRunThrowsOrNotIfCancelled
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource(100);
            cts.Cancel();

            try
            {
                Task.Run(() =>
                {
                    while (true) { cts.Token.ThrowIfCancellationRequested(); }
                }, cts.Token);
            }
            catch (Exception ex)
            {

            }

            Console.ReadLine();
        }
    }
}
