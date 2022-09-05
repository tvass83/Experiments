using Avalonia.Layout.Visualizer.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Layout.Visualizer
{
    public class LogParser
    {
        public List<ParsedObject> Parse(string serialized)
        {
            return JsonConvert.DeserializeObject<List<ParsedObject>>(serialized);
        }
    }
}
