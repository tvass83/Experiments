using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using System;
using System.Threading;

namespace AvaloniaApplication18_ToolTip_BindingFromCode
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

        public double? Percent
        {
            get { return GetValue(PercentProperty); }
            set { SetValue(PercentProperty, value); }
        }

        public static readonly StyledProperty<double?> PercentProperty =
            AvaloniaProperty.Register<MainWindow, double?>(nameof(Percent));

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            var timer = new DispatcherTimer(TimeSpan.FromMilliseconds(100), DispatcherPriority.Normal, TimerCallback);
            timer.Start();

            var btn = this.FindControl<Button>("btn1");

            btn.Bind(ToolTip.TipProperty, new Binding(nameof(MainWindow.Percent))
            {
                Converter = new Converter1(),
                ConverterParameter = "gg",
                Source = this,
                Mode = BindingMode.OneWay
            });
            
        }

        private void TimerCallback(object sender, EventArgs args)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                Percent = DateTime.Now.Ticks;
            });
        }

    }
}
