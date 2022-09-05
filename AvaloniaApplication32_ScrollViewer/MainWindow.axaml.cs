using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using System;

namespace AvaloniaApplication32_ScrollViewer
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

            var a = this.FindControl<ScrollViewer>("scrollViewer1");
            a.ScrollChanged += A_ScrollChanged;

            DispatcherTimer.RunOnce(() =>
            {
                //a.Offset = new Vector(20, 0);
                var canvas = a.Content as Canvas;
                canvas.Width *= 1.2;
            }, TimeSpan.FromSeconds(5)); ;

        }

        private void A_ScrollChanged(object? sender, ScrollChangedEventArgs e)
        {
            
        }
    }
}
