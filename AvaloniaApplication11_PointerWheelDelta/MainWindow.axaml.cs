using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication11_PointerWheelDelta
{
    public class MainWindow : Window
    {
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
        }

        private void PointerWheelChangedHandler(object sender, PointerWheelEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine($"TV: Delta: {args.Delta}");
        }
    }
}
