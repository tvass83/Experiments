using Avalonia.Threading;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace AvaloniaApp2
{
    public class ViewModel : ReactiveObject
    {
        public ViewModel()
        {
            TestCommand = ReactiveCommand.Create(OnTest);
        }

        public ReactiveCommandBase<Unit, Unit> TestCommand { get; }

        private void OnTest()
        {

        }
    }
}
