using Avalonia;
using Avalonia.Threading;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaApp65_FodyIssues
{
    public class ViewModel : ReactiveObject
    {
        public ViewModel()
        {
            Issue1();
            //Issue2();
        }

        private void Issue1()
        {
            // Throws System.InvalidProgramException: Common Language Runtime detected an invalid program
            // See https://github.com/reactiveui/ReactiveUI/issues/2688
            var t = new Test();
        }

        private void Issue2()
        {
            // Throws System.MissingFieldException: Field not found: 'AvaloniaApp65_FodyIssues.GenTest`1.<Flag>k__BackingField'
            // See https://github.com/reactiveui/ReactiveUI/issues/2416
            var t = new GenTest<bool>();
        }
    }

    class Test : ReactiveObject
    {
        [Reactive]
        public bool? Value { get; set; } = null;
    }

    class GenTest<T> : ReactiveObject
    {
        [Reactive]
        public bool Flag { get; set; } = true;
    }
}
