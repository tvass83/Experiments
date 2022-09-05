using System;
using System.Threading.Tasks;

namespace ConsoleApp1_TaskOfTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var t = new Tomi();
            t.ReturnTaskOfTask().Wait();

            Console.WriteLine("Exiting");
            Console.ReadLine();
        }

        class Tomi
        {
            public async Task<Task> ReturnTaskOfTask()
            {
                await Task.Delay(1000);

                return Task.Run(() =>
                {
                    Console.WriteLine("inner task finished");
                });
            }
        }
    }
}
