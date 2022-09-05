using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Layout.Visualizer.Model
{
    public class ParsedObject
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime TimeStamp { get; set; }
        public ParsedEventType EventType { get; set; }
        public string Tag { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
    }
}
