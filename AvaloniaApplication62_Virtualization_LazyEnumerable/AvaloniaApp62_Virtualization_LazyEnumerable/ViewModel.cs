using Avalonia.Threading;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace AvaloniaApp62_Virtualization_LazyEnumerable
{
    public class ViewModel : ReactiveObject
    {
        private List<string> _internalValues;

        public ViewModel()
        {
            BreakCommand = ReactiveCommand.Create(OnBreak);

            _internalValues = new List<string>();
            _internalValues.AddRange(new[] { "Tom1", "Tom2" });

            // Just a stupid filter to test lazy evaluation
            Values = _internalValues.Where(_ => _.StartsWith("T"));
        }

        public ReactiveCommandBase<Unit, Unit> BreakCommand { get; }

        public IEnumerable<string> Values { get; }

        private void OnBreak()
        {
            _internalValues.Clear();
        }
    }
}
