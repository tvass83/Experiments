using System;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaApp66_AsyncInit
{
    public class AsyncInitHelper : AsyncViewModelBase
    {
        private readonly Func<CancellationToken, Task> _onInitAsync;

        public AsyncInitHelper(Func<CancellationToken, Task> onInitAsync)
        {
            _onInitAsync = onInitAsync;
        }

        protected override Task OnInitializeAsync(CancellationToken ct)
        {
            return _onInitAsync(ct);
        }
    }
}
