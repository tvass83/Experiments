using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using System;

namespace AvaloniaApplication12_Slider_IsMoveToPointEnabled
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

            var slider = this.FindControl<Slider>("slider1");
            slider.EffectiveViewportChanged += Slider_EffectiveViewportChanged;

            slider.AddHandler(Thumb.DragStartedEvent, (object sender, VectorEventArgs args) =>
            {
                System.Diagnostics.Debug.WriteLine($"TV: Drag started");
            });

            slider.AddHandler(Thumb.DragCompletedEvent, (object sender, VectorEventArgs args) =>
            {
                System.Diagnostics.Debug.WriteLine($"TV: Drag stopped");
            });
        }

        private void Slider_EffectiveViewportChanged(object? sender, Avalonia.Layout.EffectiveViewportChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"TV: new size => {e.EffectiveViewport}");
        }
    }
}
