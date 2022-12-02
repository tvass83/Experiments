using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Threading;
using Avalonia.VisualTree;
using System;
using System.Linq;

namespace Synergy.Controls
{
    public sealed class BusySpinner2 : ContentControl
    {
        private DispatcherTimer _animationTimer;
        private RotateTransform _rotateTransform;
        private Ellipse[] _ellipses;
        private bool _enableStepMode = true;

        static BusySpinner2()
        {
            IsVisibleProperty.OverrideDefaultValue<BusySpinner2>(false);
        }

        public static readonly DirectProperty<BusySpinner2, bool> EnableStepModeProperty =
            AvaloniaProperty.RegisterDirect<BusySpinner2, bool>(nameof(EnableStepMode), o => o.EnableStepMode, (o, v) => o.EnableStepMode = v);

        public bool EnableStepMode
        {
            get => _enableStepMode;
            set => SetAndRaise(EnableStepModeProperty, ref _enableStepMode, value);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            var _viewbox = e.NameScope.Find("PART_Spinner") as Visual;
            _rotateTransform = _viewbox.RenderTransform as RotateTransform;

            _ellipses = _viewbox.GetVisualDescendants().OfType<Ellipse>().ToArray();

            _animationTimer = new DispatcherTimer();
            _animationTimer.Interval = TimeSpan.FromMilliseconds(75);

            if (EnableStepMode)
            {
                _animationTimer.Tick += OnAnimationTimerTick;

                this.GetObservable(IsVisibleProperty)
                   .Subscribe(isVisible =>
                   {
                       if (isVisible)
                           _animationTimer.Start();
                       else
                           _animationTimer.Stop();
                   });
            }
        }

        private void OnAnimationTimerTick(object? sender, EventArgs e)
        {
            //_rotateTransform.Angle = (_rotateTransform.Angle + 36) % 360;
            foreach (var ellipse in _ellipses)
            {
                ellipse.Opacity = GetNextOpacity(ellipse.Opacity);
            }
        }

        private double GetNextOpacity(double currentOpacity)
        {
            double step = 0.1;

            currentOpacity -= step;

            if (currentOpacity < 0.05)
            {
                currentOpacity = 1.0;
            }

            return currentOpacity;
        }
    }
}