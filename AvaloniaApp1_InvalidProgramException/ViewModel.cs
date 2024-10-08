﻿using Avalonia;
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

namespace AvaloniaApp1_InvalidProgramException
{
    public class ViewModel : ReactiveObject
    {
        public ViewModel()
        {
            TestCommand = ReactiveCommand.Create(OnTest);
        }

        public ReactiveCommandBase<Unit, Unit> TestCommand { get; }

        public static string SearchText { get; } = "tomi";

        private void OnTest()
        {
            var a = AvaloniaScheduler.Instance;
            var b = RxApp.MainThreadScheduler;
        }
    }
}
