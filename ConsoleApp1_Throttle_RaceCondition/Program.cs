using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1_Throttle_RaceCondition
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test1();

            double d = 100;

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("hu");

            for (int i = 0; i < 50; i++)
            {
                float f = 1.00f + i * 0.01f;
                Console.WriteLine(f.ToString());
            }

            Console.WriteLine(1.32d.ToString());

            Console.ReadLine();
        }

        private static async Task Test1()
        {
            while (true)
            {
                var source = Observable.Create<int>(o =>
                {
                    o.OnNext(1);
                    o.OnNext(2);
                    return Disposable.Empty;
                });

                var i = 0;
                try
                {
                    while (true)
                    {
                        i++;

                        await source
                          .Throttle(TimeSpan.Zero)
                          .Timeout(TimeSpan.FromMilliseconds(500))
                          .Take(1);
                    }
                }
                catch (TimeoutException)
                {
                    Console.WriteLine($"Failed after {i} iterations");
                }
            }
        }

        private static async Task Test2()
        {
            while (true)
            {
                var source = Observable.Create<int>(async o =>
                {
                    o.OnNext(1);

                    await Task.Delay(100);

                    o.OnNext(2);
                    return Disposable.Empty;
                });

                var i = 0;
                try
                {
                    while (true)
                    {
                        i++;

                        await source
                          .Throttle(TimeSpan.FromMilliseconds(110))
                          .Timeout(TimeSpan.FromMilliseconds(500))
                          .Take(1);
                    }
                }
                catch (TimeoutException)
                {
                    Console.WriteLine($"Failed after {i} iterations");
                }
            }
        }
    }
}
