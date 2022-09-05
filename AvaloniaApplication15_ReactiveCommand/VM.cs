using Avalonia.Threading;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvaloniaApplication15_ReactiveCommand
{
    public class VM : ReactiveObject
    {
        public VM()
        {
            SkipSecondsCommand = ReactiveCommand.Create<int>(o =>  Do(TimeSpan.FromSeconds(o)), outputScheduler: AvaloniaScheduler.Instance);
        }

        public ICommand SkipSecondsCommand { get; }

        private void Do(TimeSpan ts)
        {
            System.Diagnostics.Debug.WriteLine($"TV: {ts}");
        }
    }
}
