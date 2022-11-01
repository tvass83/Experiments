using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Threading;
using System;

namespace Synergy.Controls
{
    public sealed class BusySpinner2 : ContentControl
    {
        private DispatcherTimer _animationTimer;
        private RotateTransform _rotateTransform;

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
            _rotateTransform.Angle = (_rotateTransform.Angle + 36) % 360;
        }
    }
}