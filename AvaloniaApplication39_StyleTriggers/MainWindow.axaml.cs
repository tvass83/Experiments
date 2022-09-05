using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Diagnostics;

namespace AvaloniaApplication39_StyleTriggers
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

            var btn = this.Find<Button>("_button1");
            btn.GetPropertyChangedObservable(BackgroundProperty)
               .Subscribe(OnNext);

            var style = new Style(s => s.OfType<Button>().Class("pakk"));
            var setter = new Setter(BackgroundProperty, Brushes.Pink);

            style.Setters.Add(setter);
            btn.Styles.Add(style);

            
        }

        private void OnNext(AvaloniaPropertyChangedEventArgs args)
        {
            Debug.WriteLine($"TV: {args.Priority}");
        }
    }
}
