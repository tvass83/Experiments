using Avalonia.Controls.Primitives;
using System;

namespace AvaloniaApplication10_EventSubscriptionInAxaml
{
    public class Control1 : TemplatedControl
    {
        public delegate void MagicEventHandler(object sender, AssetEventArgs args);

        public event MagicEventHandler MagicEvent;

        public void RaiseEvent()
        {
            MagicEvent?.Invoke(this, new AssetEventArgs("asset1"));
        }
    }
}
