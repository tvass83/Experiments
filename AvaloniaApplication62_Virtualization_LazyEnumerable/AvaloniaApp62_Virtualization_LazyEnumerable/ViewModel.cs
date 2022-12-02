using Avalonia.Layout;
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
        private int _selectedTabIndex;
        private IEnumerable<string> _values;
        private List<string> _internalValues;

        public ViewModel()
        {
            _internalValues = new List<string>();

            // Just a stupid filter to test lazy evaluation
            _values = GetLazyValues();

            this.WhenAnyValue(_ => _.SelectedTabIndex)
                .Skip(1)
                .Subscribe(_ =>
                {
                    _internalValues.Clear();

                    AvaloniaScheduler.Instance.Schedule(TimeSpan.FromMilliseconds(1), () =>
                    {
                        _internalValues.AddRange(new[] { "Tom1", "Tom2" });
                        Values = GetLazyValues();
                    });
                });
        }

        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedTabIndex, value);
        }

        public IEnumerable<string> Values
        {
            get => _values;
            set => this.RaiseAndSetIfChanged(ref _values, value);
        }

        private IEnumerable<string> GetLazyValues()
        {
            return _internalValues.Where(_ => _.StartsWith("T"));
        }
    }
}
