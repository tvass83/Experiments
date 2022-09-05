using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication10_EventSubscriptionInAxaml
{
    public class AssetEventArgs : EventArgs
    {
        public AssetEventArgs()
        {

        }
        public AssetEventArgs(string assetId)
        {
            AssetId = assetId;
        }

        public string AssetId { get; }
    }
}
