using Avalonia.Threading;
using ReactiveUI;
using System;
using System.Reactive;
using System.Threading;

namespace AvaloniaApp66_AsyncInit
{
    public class MainViewModel : ReactiveObject
    {
        private ViewModel _vm1;
        private ViewModel2 _vm2;

        public MainViewModel()
        {
            _vm1 = new ViewModel();
            _vm2 = new ViewModel2();
            TestCommand1 = ReactiveCommand.Create(OnTest1);
            TestCommand2 = ReactiveCommand.Create(OnTest2);
        }

        public ReactiveCommandBase<Unit, Unit> TestCommand1 { get; }
        public ReactiveCommandBase<Unit, Unit> TestCommand2 { get; }

        private async void OnTest1()
        {
            var cts = new CancellationTokenSource();

            try
            {
                await _vm1.InitializeAsync(cts.Token);
            }
            catch (OperationCanceledException)
            { }
        }

        private async void OnTest2()
        {
            var cts = new CancellationTokenSource();

            try
            {
                await _vm2.InitializeAsync(cts.Token);
            }
            catch (OperationCanceledException)
            { }
        }
    }
}
