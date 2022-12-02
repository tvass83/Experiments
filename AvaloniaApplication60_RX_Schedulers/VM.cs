using Avalonia.Threading;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaApplication60_RX_Schedulers
{
    public class VM : ReactiveObject
    {
        public VM()
        {
            PushValueCommand = ReactiveCommand.Create(OnPushValue);
            ResetCommand = ReactiveCommand.Create(OnReset);

            OnReset();
        }

        public IReactiveCommand PushValueCommand { get; }
        public IReactiveCommand ResetCommand { get; }

        private IDisposable CreateSubscription()
        {
            return _subject
                .Do(guid => Debug.WriteLine($"TV: Thread #{Environment.CurrentManagedThreadId} OnNext() called"))
                .ObserveOn(TaskPoolScheduler.Default)
                .Do(guid => Debug.WriteLine($"TV: Thread #{Environment.CurrentManagedThreadId} ObserveOn() called"))
                .Select(_ =>
                {
                    Debug.WriteLine($"TV: Thread #{Environment.CurrentManagedThreadId} Select()");

                    return Observable.FromAsync(async () =>
                    {
                        Debug.WriteLine($"TV: Thread #{Environment.CurrentManagedThreadId} FromAsync()");
                        await Task.Delay(5000);
                        Debug.WriteLine($"TV: Thread #{Environment.CurrentManagedThreadId} After Delay()");
                        Debug.WriteLine($"TV: ...");
                    });
                })
                .Concat()
                .Subscribe();
        }

        private void OnPushValue()
        {
            //var a = AvaloniaScheduler.Instance;
            //var b = RxApp.MainThreadScheduler;

            //ThreadPool.QueueUserWorkItem(o =>
            //{
                _subject.OnNext(Guid.NewGuid());
            //});
        }

        private void OnReset()
        {
            _subscription?.Dispose();
            _subscription = CreateSubscription();
        }

        private IDisposable _subscription;
        private Subject<Guid> _subject = new Subject<Guid>();
    }
}
