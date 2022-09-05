using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace AvaloniaApplication1_LinearGradientBrush
{
    public class MainWindow : Window
    {
        private LinearGradientBrush _brush;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            Color startColor = Colors.Black;
            Color endColor = Colors.Yellow;
            double percent = 50.0;

            _brush = new LinearGradientBrush()
            {
                StartPoint = new RelativePoint(new Point(0, 0), RelativeUnit.Relative),
                EndPoint = new RelativePoint(new Point(1, 0), RelativeUnit.Relative)
            };

            _brush.GradientStops.Add(new GradientStop(startColor, 0));
            _brush.GradientStops.Add(new GradientStop(startColor, percent / 100));
            _brush.GradientStops.Add(new GradientStop(endColor, percent / 100));
            _brush.GradientStops.Add(new GradientStop(endColor, 1));

            var btn = this.FindControl<Button>("button1");
            btn.Background = _brush;
        }
    }
}
