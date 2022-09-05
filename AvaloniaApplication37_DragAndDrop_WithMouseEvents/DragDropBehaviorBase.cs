using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.VisualTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication37_DragAndDrop
{
    public abstract class BehaviorBase<T> where T : Control
    {
        public BehaviorBase(T control)
        {
            AssociatedObject = control;
        }

        public void Attach()
        {
            OnAttached();
        }

        public void Detach()
        {
            OnDetached();
        }

        protected T AssociatedObject { get; set; }

        protected abstract void OnAttached();

        protected abstract void OnDetached();
    }

    public abstract class DragDropBehaviorBase<T> : BehaviorBase<T> where T : Control
    {
        public DragDropBehaviorBase(T control) : base(control)
        {
            _translateTransform = new TranslateTransform(0, 0);
        }

        public Control DragCue { get; set; }

        protected override void OnAttached()
        {
            AssociatedObject.PointerMoved += AssociatedObject_PointerMoved;
            AssociatedObject.PointerLeave += AssociatedObject_PointerLeave;

        }

        protected override void OnDetached()
        {
            AssociatedObject.PointerMoved -= AssociatedObject_PointerMoved;
            AssociatedObject.PointerLeave -= AssociatedObject_PointerLeave;
        }

        private async void AssociatedObject_PointerMoved(object? sender, PointerEventArgs e)
        {
            if (_isDragging)
            {
                var visual = ((IVisual)e.Source).FindAncestorOfType<DataGridRow>(true);

                if (visual != null)
                {
                    Matrix? transform = visual.TransformToVisual(AssociatedObject);
                    Point p = new Point(0, 0);
                    var tp = p.Transform(transform.Value);
                    _translateTransform.X = tp.X;
                    _translateTransform.Y = tp.Y + AssociatedObject.Margin.Top;

                    var newP = visual.TranslatePoint(default(Point), AssociatedObject);

                    DragCue.RenderTransform = _translateTransform;
                    DragCue.IsVisible = true;

                    var p0 = e.GetPosition(visual);
                    var p1 = visual.PointToScreen(p0);
                    _draggedControl.Position = new PixelPoint(p1.X + 15, p1.Y + 15);
                }
                else
                {
                    //
                }

                return;
            }

            var props = e.GetCurrentPoint((T)sender).Properties;

            if (props.IsLeftButtonPressed)
            {
                var visual = ((IVisual)e.Source).FindAncestorOfType<DataGridRow>(true);
                var vm = visual?.DataContext as VM;

                if (vm != null)
                {
                    // Package the data.
                    var data = new DataObject();
                    data.Set("payload", vm);

                    // Inititate the drag-and-drop operation.
                    System.Diagnostics.Debug.WriteLine("TV: Starting drag-drop");
                    _lastPressedValue = _isDragging;
                    _isDragging = true;

                    _draggedControl = new Window() { Background = Brushes.Red };
                    _draggedControl.ShowInTaskbar = false;
                    _draggedControl.IsHitTestVisible = false;
                    _draggedControl.SizeToContent = SizeToContent.WidthAndHeight;
                    _draggedControl.Content = RenderBitmapInto(visual);
                    _draggedControl.SystemDecorations = SystemDecorations.None;

                    var p = e.GetPosition(visual);
                    var p1 = visual.PointToScreen(p);

                    //visual.IsVisible = false;

                    _draggedControl.Position = new PixelPoint(p1.X + 15, p1.Y + 15);
                    _draggedControl.Show();

                    //e.Pointer.Capture(AssociatedObject);
                    //e.Pointer.Capture(null);

                    //visual.IsVisible = true;
                    _draggedControl.Close();
                    _isDragging = false;
                    DragCue.IsVisible = false;
                    _translateTransform.X = 0;
                    _translateTransform.Y = 0;
                    System.Diagnostics.Debug.WriteLine("TV: Ending drag-drop");
                }
            }
        }

        private void AssociatedObject_PointerLeave(object? sender, PointerEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"TV: PointerLeave");
        }
        private Image RenderBitmapInto(Control visual)
        {
            var size = visual.Bounds.Size;
            var img = new Image();

            var bmp = new RenderTargetBitmap(new PixelSize((int)size.Width + 1, (int)size.Height + 1));
            var size2 = new Size(bmp.PixelSize.Width, bmp.PixelSize.Height);
            var size3 = visual.DesiredSize;



            //visual.InvalidateMeasure();
            //visual.InvalidateArrange();


            //var grid = new Grid();
            //var b = new VisualBrush(visual);
            //grid.Background = b;
            //grid.Width = size.Width;
            //grid.Height = size.Height;

            visual.InvalidateMeasure();
            visual.InvalidateArrange();
            visual.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            visual.Arrange(visual.Bounds);

            bmp.Render(visual);

            img.Source = bmp;
            img.Width = bmp.PixelSize.Width;
            img.Height = bmp.PixelSize.Height;


            return img;
        }

        private Window _draggedControl;
        private bool _dragFlag;
        private bool _isDragging;
        private bool _lastPressedValue;
        private TranslateTransform _translateTransform;
    }

    public class TestDragDrop : DragDropBehaviorBase<DataGrid>
    {
        public TestDragDrop(DataGrid grid) : base(grid)
        {

        }

        protected override void OnAttached()
        {
            base.OnAttached();
        }

        protected override void OnDetached()
        {
            base.OnDetached();
        }
    }
}
