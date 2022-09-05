using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication2_DesignDataContext
{
    public class VM
    {
        public VM()
        {
            
        }

        public static VM Instance { get; } = new VM();

        public string ContentText { get; set; } = "Test app";
    }
}
