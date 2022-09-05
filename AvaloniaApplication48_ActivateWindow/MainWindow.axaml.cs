using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using System;
using System.Diagnostics;

namespace AvaloniaApplication48_ActivateWindow
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

            var timer = new DispatcherTimer(TimeSpan.FromSeconds(5), DispatcherPriority.Normal, (sender, args) =>
            {
                (sender as DispatcherTimer).Stop();
                Activate();
            });

            timer.Start();
        }
    }
}
