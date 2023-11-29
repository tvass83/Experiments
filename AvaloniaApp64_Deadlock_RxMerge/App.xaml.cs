using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using ReactiveUI;

namespace AvaloniaApp64_Deadlock_RxMerge
{
    public class App : Application
    {
        public override void Initialize()
        {
            RxApp.MainThreadScheduler = AvaloniaScheduler.Instance;

            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
