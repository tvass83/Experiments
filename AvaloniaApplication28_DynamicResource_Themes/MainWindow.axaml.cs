using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Themes.Fluent;
using System.Linq;

namespace AvaloniaApplication28_DynamicResource_Themes
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

        private void MainWindow_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            //var style = Application.Current.Styles.First() as FluentTheme;

            var styles = Application.Current.Styles;
            styles.Clear();

            var style = new FluentTheme(new System.Uri("avares://AvaloniaApplication28_DynamicResource_Themes/App.axaml"));

            if (_mode == FluentThemeMode.Light)
            {
                _mode = FluentThemeMode.Dark;
            }
            else
            {
                _mode = FluentThemeMode.Light;
            }

            style.Mode = _mode;
            styles.Add(style);
        }

        private FluentThemeMode _mode = FluentThemeMode.Light;
    }
}
