using Avalonia;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaApp66_AsyncInit
{
    public class ViewModel : AsyncViewModelBase
    {
        protected override Task OnInitializeAsync(CancellationToken ct)
        {
            return Task.Delay(1000, ct);
        }
    }

    public class ViewModel2 : ReactiveObject, IInitializeAsync
    {
        private readonly AsyncInitHelper _helper;

        public ViewModel2()
        {
            _helper = new AsyncInitHelper(InitImplAsync);
        }

        public bool IsInitialized => _helper.IsInitialized;

        public Task InitializeAsync(CancellationToken ct)
        {
            return _helper.InitializeAsync(ct);
        }

        private Task InitImplAsync(CancellationToken ct)
        {
            return Task.Delay(1000, ct);
        }
    }
}
