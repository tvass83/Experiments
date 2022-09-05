using System;

namespace ConsoleApp1_RoslynAnalyzers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class Tomi
    {
        private int _i;

        public Tomi(int i)
        {
            _i = i;
        }

        public void DoStuff(Tomi t)
        {
            Console.WriteLine(t._i);
        }
    }
}
