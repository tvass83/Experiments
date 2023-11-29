using Avalonia;
using Avalonia.Threading;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaApp64_Deadlock_RxMerge
{
    public class ViewModel : ReactiveObject
    {
        public ViewModel()
        {
            TestCommand = ReactiveCommand.Create(OnTest);

            //EncodeStatusChanged
            Observable.Merge(EncodesStarted, EncodeComplete)
                .Do(_ => Debug.WriteLine($"TV: Merge emits new value"))
                .Subscribe(_ =>
                {
                    Debug.WriteLine($"TV: Waiting for lock in Merge");

                    lock (_encodeLock)
                    {
                        Debug.WriteLine($"TV: Acquired lock in Merge!");
                    }
                });
        }

        public IReactiveCommand TestCommand { get; }

        private void OnTest()
        {
            var a = AvaloniaScheduler.Instance;
            var b = RxApp.MainThreadScheduler;
            {
                // ProcessQueue
                Task.Run(() =>
                {
                    lock (_encodeLock)
                    {
                        Debug.WriteLine($"TV: Acquired lock in ProcessQueue!");

                        Thread.Sleep(1000);

                        Debug.WriteLine($"TV: EncodesStarted emits new value");
                        EncodesStarted.OnNext(null);
                    }
                });

                // EncodeComplete
                Task.Run(() =>
                {
                    Debug.WriteLine($"TV: EncodeComplete emits new value");
                    EncodeComplete.OnNext(null);
                });
            }
        }

        private object _encodeLock = new();

        private Subject<object> EncodesStarted = new();
        private Subject<object> EncodeComplete = new();
    }
}
