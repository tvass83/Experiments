using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace AvaloniaApplication50_RX_Throttle
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

            //#region Hidden stuff
            ////Task.Run(() =>
            //{
            //    DispatcherTimer.RunOnce(() =>
            //    {
            //        Debug.WriteLine($"TV: {DateTime.Now:HH:mm:ss.fff}");
            //    }, TimeSpan.FromMilliseconds(500));
            //}
            ////});
            //#endregion

            var obs = Observable.Timer(TimeSpan.FromMilliseconds(200), TimeSpan.FromMilliseconds(200))
                                .Throttle(TimeSpan.FromMilliseconds(50))
                                .ObserveOn(RxApp.MainThreadScheduler)
                                .Take(5);

            obs.Subscribe(inst =>
            {
                Debug.WriteLine($"TV: {DateTime.Now:HH:mm:ss.fff} value: {inst}");
            });

            var files = Directory.GetFiles(@"d:\dev", "*.*", SearchOption.AllDirectories);
        }
    }
}
