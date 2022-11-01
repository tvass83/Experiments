using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Animators;
using Avalonia.Media;
using System;
using System.Diagnostics;

namespace Synergy.Controls
{
    /// <summary>
    /// Avalonia 0.10.7
    /// https://github.com/AvaloniaUI/Avalonia/pull/6082
    /// </summary>
    public class DiscreteDoubleAnimator : Animator<double>
    {
        public override double Interpolate(double progress, double oldValue, double newValue)
        {
            int step = 36;
            var length = ((int)(progress * newValue)) / step;

            Debug.WriteLine($"TV: {length * step}");

            return length * step;
        }

        public override IDisposable Apply(Animation animation, Animatable control, IClock clock, IObservable<bool> obsMatch, Action onComplete)
        {
            Visual ctrl = (Visual)control;

            if (ctrl.RenderTransform == null)
            {
                ctrl.RenderTransform = new RotateTransform();
            }

            return base.Apply(animation, (Transform)ctrl.RenderTransform, clock, obsMatch, onComplete);
        }
    }
}
