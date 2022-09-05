using Avalonia;
using Avalonia.Controls;
using Avalonia.Metadata;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication40_DesignerSupport
{
    public class DesignerSupport
    {
        public static readonly AttachedProperty<IEnumerable<IStyle>> DesignStylesProperty =
            AvaloniaProperty.RegisterAttached<DesignerSupport, Control, IEnumerable<IStyle>>("DesignStyles", defaultValue: new List<IStyle>());

        //private static bool Validate(IEnumerable<IStyle> styles)
        //{
        //    return true;
        //}

        //private static IEnumerable<IStyle> Coerce(IAvaloniaObject obj, IEnumerable<IStyle> styles)
        //{
        //    return styles;
        //}

        public static IEnumerable<IStyle> GetDesignStyles(Control element)
        {
            var ret = element.GetValue(DesignStylesProperty);
            return ret;
        }

        public static void SetDesignStyles(Control element, IEnumerable<IStyle> styles)
        {
            if (Design.IsDesignMode)
            {
                element.Styles.AddRange(styles);
            }

            element.SetValue(DesignStylesProperty, styles);
        }
    }
}
