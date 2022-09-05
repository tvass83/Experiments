using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;

namespace AvaloniaApplication14_PropertyChangedCallback
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            var ctrl = this.FindControl<Control1>("c1");
            ctrl.GetObservable(Control1.FooProperty)
                .Subscribe(FooChanged);
        }

        private void FooChanged(int i)
        {

        }
    }
}
