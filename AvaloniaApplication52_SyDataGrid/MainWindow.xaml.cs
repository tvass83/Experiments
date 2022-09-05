using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Styling;
using Avalonia.Utilities;
using Avalonia.VisualTree;
using System;
using System.Linq;

namespace AvaloniaApplication52_SyDataGrid
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

            Test1();
        }

        private void MeasureStuff()
        {

            //var popup = new Popup();
            //var tb = new TextBlock() { Text = "Garcia, D. (EH) replaces Aldrete, C. (RF) bat. 7th\r\nAldrete, C. (RF) replaces Austin, C. (DH) bat. 8th\r\nDenson, M. (2B) replaces Swift, D. (2B) bat. 9th" };
            //tb.Measure(new Size(1, double.MaxValue));

            var itemsControl = new ItemsControl();

            var wnd = new Window();
            wnd.Content = itemsControl;

            //var ct = new DataTemplate();
            //ct.TargetType = typeof(ListBoxItem);
            //ct.Content = tb;
            itemsControl.DataTemplates.Add(new FuncDataTemplate<int>((control, name) => CreateSP(control, name)));

            itemsControl.ItemsPanel = new FuncTemplate<IPanel>(() => new VirtualizingStackPanel());
            itemsControl.Items = Enumerable.Range(0, 2);

            itemsControl.Measure(Size.Infinity);

            var des = itemsControl.FindDescendantOfType<TextBlock>().DesiredSize;
            var des2 = itemsControl.DesiredSize;

            var containers = itemsControl.ItemContainerGenerator.Containers;

            int i = 5;

            //var wnd = new Window();
            //var control = new ListBox();
            //control.Height = 1000;
            //control.VirtualizationMode = ItemVirtualizationMode.None;
            //wnd.Content = control;
            //control.Items = Enumerable.Range(100, 120);
            //control.Measure(Size.Infinity);
            //control.Arrange(new Rect(0, 0, control.DesiredSize.Width, control.DesiredSize.Height));
        }

        private void Test1()
        {
            var dp = new DockPanel();
            dp.Width = 0;

            var res = ApplyLayoutConstraints(dp, Size.Infinity);
        }

        private Size ApplyLayoutConstraints(ILayoutable control, Size constraints)
        {
            MinMax minmax = new MinMax(control);
            return new Size(MathUtilities.Clamp(constraints.Width, minmax.MinWidth, minmax.MaxWidth), MathUtilities.Clamp(constraints.Height, minmax.MinHeight, minmax.MaxHeight));
        }

        private readonly struct MinMax
        {
            public double MinWidth { get; }

            public double MaxWidth { get; }

            public double MinHeight { get; }

            public double MaxHeight { get; }

            public MinMax(ILayoutable e)
            {
                MaxHeight = e.MaxHeight;
                MinHeight = e.MinHeight;
                double i = e.Height;
                double height = (double.IsNaN(i) ? double.PositiveInfinity : i);
                MaxHeight = Math.Max(Math.Min(height, MaxHeight), MinHeight);
                height = (double.IsNaN(i) ? 0.0 : i);
                MinHeight = Math.Max(Math.Min(MaxHeight, height), MinHeight);
                MaxWidth = e.MaxWidth;
                MinWidth = e.MinWidth;
                i = e.Width;
                double width = (double.IsNaN(i) ? double.PositiveInfinity : i);
                MaxWidth = Math.Max(Math.Min(width, MaxWidth), MinWidth);
                width = (double.IsNaN(i) ? 0.0 : i);
                MinWidth = Math.Max(Math.Min(MaxWidth, width), MinWidth);
            }
        }

        private Control CreateSP(int dc, INameScope ns)
        {
            var sp = new StackPanel();
            
            var tb = new TextBlock()
            {
                Text = "Garcia, D. (EH) replaces Aldrete, C. (RF) bat. 7th\r\nAldrete, C. (RF) replaces Austin, C. (DH) bat. 8th\r\nDenson, M. (2B) replaces Swift, D. (2B) bat. 9th"
            };

            sp.Children.Add(tb);

            var dp = new DockPanel();
            dp.Children.Add(sp);

            return dp;
        }

        //private void Prepare(ListBox target)
        //{
        //    // The ListBox needs to be part of a rooted visual tree.
        //    var root = new TestRoot();
        //    root.Child = target;

        //    // Apply the template to the ListBox itself.
        //    target.ApplyTemplate();

        //    // Then to its inner ScrollViewer.
        //    var scrollViewer = (ScrollViewer)target.GetVisualChildren().Single();
        //    scrollViewer.ApplyTemplate();

        //    // Then make the ScrollViewer create its child.
        //    ((ContentPresenter)scrollViewer.Presenter).UpdateChild();

        //    // Now the ItemsPresenter should be reigstered, so apply its template.
        //    target.Presenter.ApplyTemplate();

        //    // Because ListBox items are virtualized we need to do a layout to make them appear.
        //    target.Measure(new Size(100, 100));
        //    target.Arrange(new Rect(0, 0, 100, 100));

        //    // Now set and apply the item templates.
        //    foreach (ListBoxItem item in target.Presenter.Panel.Children)
        //    {
        //        item.Template = ListBoxItemTemplate();
        //        item.ApplyTemplate();
        //        item.Presenter.ApplyTemplate();
        //        ((ContentPresenter)item.Presenter).UpdateChild();
        //    }

        //    // The items were created before the template was applied, so now we need to go back
        //    // and re-arrange everything.
        //    foreach (IControl i in target.GetSelfAndVisualDescendants())
        //    {
        //        i.InvalidateMeasure();
        //    }

        //    target.Measure(new Size(100, 100));
        //    target.Arrange(new Rect(0, 0, 100, 100));
        //}

    }
}
