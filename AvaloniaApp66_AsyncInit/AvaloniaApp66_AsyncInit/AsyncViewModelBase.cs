using ReactiveUI;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaApp66_AsyncInit
{
    public interface IInitializeAsync
    {
        Task InitializeAsync(CancellationToken ct);

        bool IsInitialized { get; }
    }

    public abstract class AsyncViewModelBase : ReactiveObject, IInitializeAsync
    {
        private bool? _isInitializing;
        private string _correlationId;

        private Func<Task> _task;
        private CancellationTokenSource _cancellationForInit;

        public bool IsInitialized => _isInitializing.HasValue && !_isInitializing.Value;

        public Task InitializeAsync(CancellationToken ct)
        {
            if (IsInitialized)
            {
                Debug.WriteLine($"TV: Already initialized");
                return Task.CompletedTask;
            }

            return ct == default ?
                Case1() :
                Case2(ct);
        }

        private async Task Case1()
        {
            if (_isInitializing.HasValue && _isInitializing.Value)
            {
                Debug.WriteLine($"TV: [{_correlationId}] Returning previous Task");
                await _task();

                return;
            }

            Debug.WriteLine($"TV: [{_correlationId}] Init for Case1 started");
            _isInitializing = true;
            _task = () => OnInitializeAsync(default);

            try
            {
                await _task();
            }
            catch
            {
                // An error occured, so let's reset the state to enable subsequent initializations
                _isInitializing = null;

                throw;
            }

            _task = null;
            _isInitializing = false;

            Debug.WriteLine($"TV: [{_correlationId}] Init for Case1 done");
        }

        private async Task Case2(CancellationToken ct)
        {
            if (_isInitializing.HasValue && _isInitializing.Value)
            {
                Debug.WriteLine($"TV: [{_correlationId}] Cancelling previous one 1");
                _cancellationForInit.Cancel();
                _cancellationForInit?.Dispose();
            }

            _cancellationForInit = CancellationTokenSource.CreateLinkedTokenSource(ct);

            CancellationToken ct2 = _cancellationForInit.Token;
            _correlationId = Guid.NewGuid().ToString();

            Debug.WriteLine($"TV: [{_correlationId}] Init started");
            _isInitializing = true;

            try
            {
                await OnInitializeAsync(ct2);
            }
            catch
            {
                // An error occured or the operation was cancelled, so let's reset the state to enable subsequent initializations
                _isInitializing = null;

                throw;
            }
            finally
            {
                _cancellationForInit.Dispose();
                _cancellationForInit = null;
            }

            _isInitializing = false;

            Debug.WriteLine($"TV: [{_correlationId}] Init done");
        }

        protected abstract Task OnInitializeAsync(CancellationToken ct);
    }
}
