using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication35_Tabs
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

        private Popup _popup;

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            _popup = this.FindControl<Popup>("popup");
        }

        private void ButtonClick(object sender, RoutedEventArgs args)
        {
            _popup.IsOpen = true;
        }
    }
}
