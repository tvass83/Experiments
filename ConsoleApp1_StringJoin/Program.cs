using System;

namespace ConsoleApp1_StringJoin
{
    class Program
    {
        static void Main(string[] args)
        {
            string s1 = "a";
            string s2 = "b";
            string sss = s1 + s2;
            Console.WriteLine(sss);

            //string s = string.Join(',', "hello", null, "tomi", "", "pakk");
            //Console.WriteLine(s);

            //var j = new John();
            //j.ValueChanged += J_ValueChanged;
            //j.RaiseEvent();
        }

        private static void J_ValueChanged(object sender, double e)
        {
            Console.WriteLine($"Event was raised: {e}");
        }
    }
    class John
    {
        public event EventHandler<double> ValueChanged;
        //public event EventHandler<DoubleEventArgs> ValueChanged;

        public void RaiseEvent()
        {
            ValueChanged?.Invoke(this, 0);
        }
    }
}
