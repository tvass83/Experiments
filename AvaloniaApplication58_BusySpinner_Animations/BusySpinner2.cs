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

        static BusySpinner2()
        {
            IsVisibleProperty.OverrideDefaultValue<BusySpinner2>(false);
        }

        public bool EnableStepMode { get; set; }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            if (EnableStepMode)
            {
                var _viewbox = e.NameScope.Find("viewbox") as Visual;
                _rotateTransform = _viewbox.RenderTransform as RotateTransform;

                _ellipses = _viewbox.GetVisualDescendants().OfType<Ellipse>().ToArray();

                _animationTimer = new DispatcherTimer();
                _animationTimer.Interval = TimeSpan.FromMilliseconds(75);
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