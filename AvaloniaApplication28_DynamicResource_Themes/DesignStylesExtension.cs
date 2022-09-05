using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;

namespace AvaloniaApplication28_DynamicResource_Themes
{
    public class DesignerSupport
    {
        public static readonly AttachedProperty<Styles> DesignStylesProperty =
            AvaloniaProperty.RegisterAttached<DesignerSupport, Control, Styles>("DesignStyles");

        public static Styles GetDesignStyles(Control element)
        {
            return element.GetValue(DesignStylesProperty);
        }

        public static void SetDesignStyles(Control element, Styles styles)
        {
            if (Design.IsDesignMode)
            {
                element.Styles.AddRange(styles);

                foreach (var item in styles.Resources.MergedDictionaries)
                {
                    element.Resources.MergedDictionaries.Add(item);
                }
            }

            element.SetValue(DesignStylesProperty, styles);
        }
    }
}
