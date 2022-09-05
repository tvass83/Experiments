using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //EnforceSingleApplicationInstance2();

            //Console.WriteLine(TimeSpan.FromSeconds(double.Parse("1863.862000", CultureInfo.GetCultureInfo("hr"))));
            //Console.WriteLine(TimeSpan.FromSeconds(double.Parse("1863,862000", CultureInfo.GetCultureInfo("hu"))));
            //Console.WriteLine(TimeSpan.FromSeconds(double.Parse("1863.862000", CultureInfo.GetCultureInfo("en"))));

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("hr");
            Console.WriteLine(ConvertFFmpegTimeSpan("00:31:03.8620000"));

            //Console.WriteLine(TimeSpan.Parse("23:57:43").TotalSeconds);
            Console.ReadLine();
        }

        private static TimeSpan ConvertFFmpegTimeSpan(string value)
        {
            Match m = _rexTimeSpan.Match(value);
            double v = 0.0;

            if (!m.Success)
                return new TimeSpan();

            if (!String.IsNullOrEmpty(m.Groups["h"].Value))
                v += Convert.ToInt32(m.Groups["h"].Value);
            v *= 60.0;

            if (!String.IsNullOrEmpty(m.Groups["m"].Value))
                v += Convert.ToInt32(m.Groups["m"].Value);
            v *= 60.0;

            if (!String.IsNullOrEmpty(m.Groups["s"].Value))
                v += Convert.ToInt32(m.Groups["s"].Value);

            if (!String.IsNullOrEmpty(m.Groups["f"].Value))
            {
                double test1 = Convert.ToDouble(string.Format("0{1}{0}", m.Groups["f"].Value, CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator));
                double test2 = Convert.ToDouble(string.Format("0{1}{0}", m.Groups["f"].Value, CultureInfo.InvariantCulture.NumberFormat.CurrencyDecimalSeparator));
                double test3 = Convert.ToDouble(string.Format("0{1}{0}", m.Groups["f"].Value, CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator), CultureInfo.InvariantCulture);

                v += test1;
            }

            return TimeSpan.FromSeconds(v);
        }

        private static readonly Regex _rexTimeSpan = new Regex(@"^(((?<h>\d+):)?(?<m>\d+):)?(?<s>\d+)([\.,](?<f>\d+))?$", RegexOptions.Compiled);

        private static void EnforceSingleApplicationInstance2()
        {
            var currentProcess = Process.GetCurrentProcess();
            string currentProcessName = currentProcess.ProcessName;

            bool ok;
            var m = new System.Threading.Mutex(true, "Synergy.BaseballLogger.Win32", out ok);

            if (!ok)
            {
                Environment.Exit(0);
            }

            GC.KeepAlive(m);
        }
    }
}
