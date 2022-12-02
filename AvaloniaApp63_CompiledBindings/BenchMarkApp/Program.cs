using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;

namespace BenchMarkApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Benchmarks>();
        }
    }

    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net472)]
    [SimpleJob(RuntimeMoniker.Net60)]
    public class Benchmarks
    {
        [Benchmark(Baseline = true)]
        public void Test1()
        {
            new AvaloniaApp1.MainWindow();
        }

        [Benchmark]
        public void Test2()
        {
            new AvaloniaApp2.MainWindow();
        }
    }
}
