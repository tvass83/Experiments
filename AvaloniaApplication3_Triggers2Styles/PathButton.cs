using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaApplication3_Triggers2Styles
{
    public class PathButton : Button
    {
        public Geometry Path
        {
            get { return GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        public static readonly StyledProperty<Geometry> PathProperty =
            AvaloniaProperty.Register<PathButton, Geometry>(nameof(Path));

        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        public static readonly StyledProperty<Brush> FillProperty =
            AvaloniaProperty.Register<PathButton, Brush>(nameof(Fill));
    }
}
