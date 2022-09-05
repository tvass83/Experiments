using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;
using System.Diagnostics;

namespace AvaloniaApplication23_LayoutPerformance
{
    public class MainWindow : Window
    {
        private Canvas _canvas;
        private ScrollViewer _scroller;

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

            _canvas = this.FindControl<Canvas>("canvas");
            _scroller = _canvas.Parent as ScrollViewer;

            _scroller.ScrollChanged += _scroller_ScrollChanged;
            _scroller.LayoutUpdated += _scroller_LayoutUpdated;

            Debug.WriteLine($"test: {Math.PI:F5}");
        }

        private void _scroller_LayoutUpdated(object? sender, EventArgs e)
        {
            
        }

        private void _scroller_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            Debug.WriteLine($"TV: OffsetDelta => {e.OffsetDelta}");

            _canvas.Width = 15000;
            HideHandler(null, null);

            var vec = new Vector(_scroller.Offset.X + 1, _scroller.Offset.Y);
            _scroller.Offset = vec;
            Debug.WriteLine($"TV: Offset => {_scroller.Offset}");            
        }

        private void ClickHandler(object sender, RoutedEventArgs args)
        {
            const int TOTAL_GRIDS = 1000;

            for (int i = 0; i < TOTAL_GRIDS; i++)
            {
                var grid = new Grid();
                grid.Background = Brushes.Black;
                grid.Width = 1;
                grid.Height = 10;

                Canvas.SetLeft(grid, i * 100);
                Canvas.SetTop(grid, 10);

                _canvas.Children.Add(grid);
            }           
        }

        private void HideHandler(object sender, RoutedEventArgs args)
        {
            foreach (var item in _canvas.Children)
            {
                item.IsVisible = false;
            }            
        }
    }
}
