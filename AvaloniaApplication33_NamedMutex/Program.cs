using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System;
using System.Diagnostics;

namespace AvaloniaApplication33_NamedMutex
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args)
        {
            EnforceSingleApplicationInstance2();

            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

            Console.ReadLine();
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();

        private static void EnforceSingleApplicationInstance2()
        {
            var currentProcess = Process.GetCurrentProcess();
            string currentProcessName = currentProcess.ProcessName;

            bool ok;
            var m = new System.Threading.Mutex(true, "Synergy.BaseballLogger.Win32", out ok);

            if (!ok)
            {
                Environment.Exit(0);
            }

            GC.KeepAlive(m);
        }
    }
}
