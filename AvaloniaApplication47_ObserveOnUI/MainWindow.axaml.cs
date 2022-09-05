using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaApplication47_ObserveOnUI
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

            var border = this.FindControl<Border>("_border");

            //The BAD way: input thread is wrong, but async methods are serialized properly
            //Observable.FromEventPattern<EventHandler<PointerEventArgs>, PointerEventArgs>(
            //       h => border.PointerEnter += h,
            //       h => border.PointerEnter -= h)
            //   .Select(_ => Observable.FromAsync(OnContentGroupChanged))
            //   .Concat()
            //   .ObserveOnUI()
            //   .Subscribe();

            //The ALMOST GOOD way: input thread is good, but async methods won't be "serialized" properly
            //Observable.FromEventPattern<EventHandler<PointerEventArgs>, PointerEventArgs>(
            //       h => border.PointerEnter += h,
            //       h => border.PointerEnter -= h)
            //   .ObserveOnUI()
            //   .SelectMany(_ => Observable.FromAsync(OnContentGroupChanged))
            //   .Subscribe();

            //The GOOD way
            Observable.FromEventPattern<EventHandler<PointerEventArgs>, PointerEventArgs>(
                   h => border.PointerEnter += h,
                   h => border.PointerEnter -= h)
               .Select(_ => Observable.FromAsync(OnContentGroupChanged, AvaloniaScheduler.Instance))
               .Concat()
               .Subscribe();
        }

        private async Task OnContentGroupChanged()
        {
            Debug.WriteLine($"TV: Before ThreadID: {Thread.CurrentThread.ManagedThreadId}");

            if (SynchronizationContext.Current == null)
            {
                Debug.WriteLine($"TV: NULL");
            }
            else
            {
                Debug.WriteLine($"TV: OK");
            }

            await Task.Delay(500);

            Debug.WriteLine($"TV: After ThreadID: {Thread.CurrentThread.ManagedThreadId}");
        }
    }

    public static class UIObservableExtensions
    {
        public static IObservable<T> ObserveOnUI<T>(this IObservable<T> observable) =>
            observable.ObserveOn(RxApp.MainThreadScheduler);
    }
}
