using Avalonia.Threading;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;

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
                .Subscribe(guid =>
                {
                    Debug.WriteLine($"TV: Thread #{Environment.CurrentManagedThreadId} ObserveOn()");
                    Debug.WriteLine($"TV: ...");
                });
        }

        private void OnPushValue()
        {
            var a = AvaloniaScheduler.Instance;
            var b = RxApp.MainThreadScheduler;

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
