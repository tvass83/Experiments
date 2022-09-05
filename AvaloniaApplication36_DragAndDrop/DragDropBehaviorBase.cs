using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.VisualTree;
using System;
using System.Collections.ObjectModel;

namespace AvaloniaApplication36_DragAndDrop
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

    public abstract class DragDropBehaviorBase<TContainer, TItem> : BehaviorBase<TContainer> where TContainer : Control where TItem : Control
    {
        public DragDropBehaviorBase(TContainer control) : base(control)
        {
            _translateTransform = new TranslateTransform(0, 0);
        }

        public Control DragCue { get; set; }

        protected override void OnAttached()
        {
            AssociatedObject.PointerMoved += AssociatedObject_PointerMoved;

            AssociatedObject.AddHandler(DragDrop.DragEnterEvent, OnDragEnter);
            AssociatedObject.AddHandler(DragDrop.DragLeaveEvent, OnDragLeave);
            AssociatedObject.AddHandler(DragDrop.DragOverEvent, OnDragOver);
            AssociatedObject.AddHandler(DragDrop.DropEvent, OnDrop);
        }

        protected override void OnDetached()
        {
            AssociatedObject.PointerMoved -= AssociatedObject_PointerMoved;

            AssociatedObject.RemoveHandler(DragDrop.DragEnterEvent, OnDragEnter);
            AssociatedObject.RemoveHandler(DragDrop.DragLeaveEvent, OnDragLeave);
            AssociatedObject.RemoveHandler(DragDrop.DragOverEvent, OnDragOver);
            AssociatedObject.RemoveHandler(DragDrop.DropEvent, OnDrop);
        }

        private void OnDragLeave(object? sender, RoutedEventArgs e)
        {
            var visual = ((IVisual)e.Source).FindAncestorOfType<TContainer>(true);

            if (visual == null)
            {
                _draggedControl.Hide();
            }
        }

        private void OnDragEnter(object? sender, DragEventArgs e)
        {
            var visual = ((IVisual)e.Source).FindAncestorOfType<TContainer>(true);

            if (visual != null)
            {
                _draggedControl.Show();
            }
        }

        protected virtual void OnDragOver(object sender, DragEventArgs e)
        {
            var visual = ((Visual)e.Source).FindAncestorOfType<TItem>(true);

            if (visual != null)
            {
                System.Diagnostics.Debug.WriteLine($"TV: type: {visual.GetType()}");

                _lastItemAtMouse = GetIndexOfDraggedItem(visual.DataContext);

                if (_lastItemAtMouse != _draggedItem)
                {
                    Matrix? transform = visual.TransformToVisual(AssociatedObject);
                    Point p;

                    if (_lastItemAtMouse > _draggedItem)
                    {
                        _insertAbove = false;
                        p = new Point(0, visual.Bounds.Height);
                    }
                    else
                    {
                        p = new Point(0, 0);
                        _insertAbove = true;
                    }

                    var tp = p.Transform(transform.Value);
                    _translateTransform.X = tp.X;
                    _translateTransform.Y = tp.Y + AssociatedObject.Margin.Top;

                    DragCue.RenderTransform = _translateTransform;
                    DragCue.IsVisible = true;

                    UpdateDraggedWindowPosition(e, visual);
                }
                else
                {
                    UpdateDraggedWindowPosition(e, visual);
                    DragCue.IsVisible = false;
                }
            }
            else
            {
                // We are over AssociatedObject in general here
                UpdateDraggedWindowPosition(e, AssociatedObject);
            }
        }

        protected void OnDrop(object sender, DragEventArgs e)
        {
            OnDrop(_draggedItem, _lastItemAtMouse);
        }

        protected abstract void OnDrop(int draggedIndex, int newIndex);

        protected abstract int GetIndexOfDraggedItem(object vm);

        private async void AssociatedObject_PointerMoved(object? sender, PointerEventArgs e)
        {
            if (_isDragging) return;

            var props = e.GetCurrentPoint((TContainer)sender).Properties;

            if (props.IsLeftButtonPressed)
            {
                var visual = ((Visual)e.Source).FindAncestorOfType<TItem>(true);
                var vm = visual?.DataContext as VM;

                if (vm != null)
                {
                    // Package the data.
                    var data = new DataObject();
                    //data.Set("payload", vm);

                    // Inititate the drag-and-drop operation.
                    System.Diagnostics.Debug.WriteLine("TV: Starting drag-drop");
                    _isDragging = true;

                    _draggedControl = new Window()
                    {
                        Background = Brushes.Black,
                        IsHitTestVisible = false,
                        Padding = new Thickness(0),
                        SizeToContent = SizeToContent.WidthAndHeight,
                        Content = RenderBitmapInto(visual),
                        VerticalAlignment = VerticalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        SystemDecorations = SystemDecorations.None,
                        ShowInTaskbar = false
                    };

                    _draggedItem = GetIndexOfDraggedItem(vm);

                    var p = e.GetPosition(visual);
                    var p1 = visual.PointToScreen(p);

                    //visual.IsVisible = false;

                    _draggedControl.Position = new PixelPoint(p1.X + 15, p1.Y + 15);
                    _draggedControl.Show();

                    //e.Pointer.Capture(AssociatedObject);
                    await DragDrop.DoDragDrop(e, data, DragDropEffects.Copy | DragDropEffects.Move);

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

            //visual.InvalidateMeasure();
            //visual.InvalidateArrange();
            //visual.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            //visual.Arrange(visual.Bounds);

            bmp.Render(visual);

            img.Source = bmp;
            img.Width = bmp.PixelSize.Width;
            img.Height = bmp.PixelSize.Height;


            return img;
        }

        private void UpdateDraggedWindowPosition(DragEventArgs e, Visual visual)
        {
            var p0 = e.GetPosition(visual);
            var p1 = visual.PointToScreen(p0);
            _draggedControl.Position = new PixelPoint(p1.X + 15, p1.Y + 15);
        }

        private Window _draggedControl;
        private bool _dragFlag;
        private bool _isDragging;
        private TranslateTransform _translateTransform;
        private int _draggedItem;
        private bool _insertAbove;
        private int _lastItemAtMouse;
    }

    public class TestDragDrop : DragDropBehaviorBase<DataGrid, DataGridRow>
    {
        public TestDragDrop(DataGrid grid) : base(grid)
        {

        }

        protected override int GetIndexOfDraggedItem(object item)
        {
            int idx = 0;

            foreach (var element in AssociatedObject.Items)
            {
                if (element == item)
                {
                    return idx;
                }

                idx++;
            }

            throw new InvalidOperationException();
        }

        protected override void OnDrop(int draggedIndex, int newIndex)
        {
            var src = AssociatedObject.Items as ObservableCollection<VM>;
            src.Move(draggedIndex, newIndex);

            AssociatedObject.Items = null;
            AssociatedObject.Items = src;
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
