using Avalonia;

namespace AvaloniaApp64_Deadlock_RxMerge
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                //Better to be more specific instead of using UsePlatformDetect()
                .UseWin32() // or UseAvaloniaNative() or UseX11()
                .With(new Win32PlatformOptions() { UseWindowsUIComposition = false })
                .UseDirect2D1() //or UseSkia()
                .LogToTrace();
    }
}
