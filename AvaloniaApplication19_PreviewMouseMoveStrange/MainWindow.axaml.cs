using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Threading;
using System;
using System.Diagnostics;

namespace AvaloniaApplication19_PreviewMouseMoveStrange
{
    public class MainWindow : Window
    {
        private Slider _slider;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        static MainWindow()
        {
            //AffectsArrange<MainWindow>(RangeBase.ValueProperty);
            //AffectsMeasure<MainWindow>(RangeBase.ValueProperty);
            //AffectsRender<MainWindow>(RangeBase.ValueProperty);

            //AffectsArrange<Slider>(RangeBase.ValueProperty);
            //AffectsMeasure<Slider>(RangeBase.ValueProperty);
            //AffectsRender<Slider>(RangeBase.ValueProperty);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            _slider = this.FindControl<Slider>("slider1");

            var timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            _slider.Value += _slider.SmallChange;
            _slider.InvalidateArrange();
            _slider.InvalidateMeasure();
            _slider.InvalidateVisual();
        }

        private void Button_PreviewMouseMove(object sender, PointerEventArgs e)
        {
            Debug.WriteLine($"TV: {DateTime.Now}");
        }

    }
}
