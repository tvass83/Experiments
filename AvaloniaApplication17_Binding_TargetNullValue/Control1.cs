using Avalonia;
using Avalonia.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication17_Binding_TargetNullValue
{
    public class Control1 : TemplatedControl
    {
        public double? Percent
        {
            get { return GetValue(PercentProperty); }
            set { SetValue(PercentProperty, value); }
        }

        public static readonly StyledProperty<double?> PercentProperty =
            AvaloniaProperty.Register<Control1, double?>(nameof(Percent));
    }
}
