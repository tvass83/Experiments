using System;
using System.Globalization;

namespace ConsoleApp1_TimeSpan_ParseExact
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataStartTime = "12:34:56.789";
            var ts = TimeSpan.ParseExact(dataStartTime, @"hh\:mm\:ss\.fff", CultureInfo.InvariantCulture);

            string s = ts.ToString(@"hh\:mm\:ss\.fff", CultureInfo.InvariantCulture);
            Console.WriteLine(s);

            //string s2 = string.Format(@"value: {0:hh\:mm\:ss\.fff}", ts);
            //Console.WriteLine(s2);
        }
    }
}
