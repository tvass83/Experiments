using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1_IEnumerable
{
    class Program
    {
        static void Main(string[] args)
        {
            //var values = new bool[] { false };

            //bool result = values.All(v => v);

            var t = new Tomi();

            Console.WriteLine(t.Prop1);
            Console.WriteLine(t.Prop1);
            Console.WriteLine(t.Prop1);

            t.Show();
        }
    }

    class Tomi
    {
        public string Prop1 { get; } = Guid.NewGuid().ToString();

        public void Show()
        {
            Console.WriteLine(_applicationDataDirectory);
            Console.WriteLine(_defaultVideoCacheFolder);
        }

        private static readonly string _defaultVideoCacheFolder = Path.Combine(_applicationDataDirectory, "VideoCache");
        private static readonly string _applicationDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SynergySports");
    }
}
