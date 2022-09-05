using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace ConsoleApp1_Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<SingleInstance>();
        }
    }

    [SimpleJob(RuntimeMoniker.Net472, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net50)]
    public class SingleInstance
    {
        [Benchmark]
        public void Test1()
        {
            string currentProcessName;

            using (var currentProcess = Process.GetCurrentProcess())
            {
                currentProcessName = currentProcess.ProcessName;
            }

            var processes = Process.GetProcessesByName(currentProcessName);
            int procs = processes.Length;

            foreach (var process in processes)
            {
                process.Dispose();
            }

            if (procs > 1)
            {
                return;
            }
        }

        [Benchmark]
        public void Test2()
        {
            string currentProcessName;

            using (var currentProcess = Process.GetCurrentProcess())
            {
                currentProcessName = currentProcess.ProcessName;
            }
            
            bool newlyCreated;
            var mutex = new Mutex(true, currentProcessName, out newlyCreated);

            if (!newlyCreated)
            {
                return;
            }

            GC.KeepAlive(mutex);
        }
    }
}
