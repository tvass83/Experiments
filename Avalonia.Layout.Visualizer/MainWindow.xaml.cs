using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Layout.Visualizer.Model;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Avalonia.Layout.Visualizer
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);

            Initialize();

            this.AttachDevTools();
        }

        private void Initialize()
        {
            var canvas = this.Get<Canvas>("canvas");
            var canvas2 = this.Get<Canvas>("canvas2");

            var p = new LogParser();
            _rawData = p.Parse(File.ReadAllText(@"d:\temp\tracked.txt"));

            Populate(canvas, false);
            //Populate(canvas2);
        }

        private void Populate(Canvas canvas, bool relativePlacement = true)
        {
            var firstEvent = _rawData.OrderBy(x => x.TimeStamp).First().TimeStamp;
            var groups = _rawData.GroupBy(x => x.Id);

            if (relativePlacement)
            {
                DrawTimeLine(canvas);
            }

            int groupIdx = -1;
            foreach (var group in groups)
            {
                groupIdx++;
                double prevLeft = 0;
                var top = TOP_MARGIN + groupIdx * TRACK_Y;

                var tb = new TextBlock();
                tb.FontSize = 10;
                tb.Text = group.First().Type;
                Canvas.SetTop(tb, top);

                canvas.Children.Add(tb);

                double increment = 0;
                int idx = -1;

                ParsedObject? prevEvent = null;

                foreach (var item in group.OrderBy(x => x.TimeStamp))
                {
                    idx++;
                    var dot = CreateDot(item);

                    var diff = MsDiff(firstEvent, item.TimeStamp);
                    double left;

                    if (relativePlacement)
                    {
                        left = DISTANCE_X * diff;
                        var oldLeft = left;

                        if (prevLeft == left)
                        {
                            increment += MIN_DISTANCE_X;
                            left += increment;
                        }
                        else
                        {
                            increment = 0;
                        }

                        prevLeft = oldLeft;
                    }
                    else
                    {
                        left = idx * MIN_DISTANCE_X;
                        double diff2 = 0;

                        if (prevEvent != null)
                        {
                            diff2 = MsDiff(prevEvent.TimeStamp, item.TimeStamp);
                        }

                        if (diff2 > 2000)
                        {
                            var border = new Border();
                            border.Width = 2;
                            border.Height = 16;
                            border.BorderThickness = new Thickness(1);
                            border.BorderBrush = Brushes.Black;

                            Canvas.SetLeft(border, LEFT_MARGIN + left);
                            Canvas.SetTop(border, top - border.Height / 4);
                            canvas.Children.Add(border);
                        }
                    }

                    Dictionary<string, string> objDiff;

                    if (prevEvent != null)
                    {
                        objDiff = prevEvent.CalcDiff(item);

                        string tooltip;

                        if (objDiff.Any())
                        {
                            dot.Fill = Brushes.Yellow;

                            tooltip = $"{item.TimeStamp:HH:mm:ss.fff}{Environment.NewLine}{string.Join(Environment.NewLine, objDiff)}";

                            tooltip = string.Format("{4}{0}{0}{2} ({3}){0}{1}",
                                                        Environment.NewLine,
                                                        string.Join(Environment.NewLine, item.Attributes),
                                                        item.EventType,
                                                        item.Tag,
                                                        tooltip);

                        }
                        else
                        {
                            tooltip = string.Format("{2:HH:mm:ss.fff}{0}{3} ({4}){0}{1}",
                                                        Environment.NewLine,
                                                        string.Join(Environment.NewLine, item.Attributes),
                                                        item.TimeStamp,
                                                        item.EventType,
                                                        item.Tag);
                        }

                        ToolTip.SetTip(dot, tooltip);
                    }

                    prevEvent = item;

                    Canvas.SetLeft(dot, LEFT_MARGIN + left);
                    Canvas.SetTop(dot, top);

                    canvas.Children.Add(dot);
                }
            }
        }

        private void DrawTimeLine(Canvas canvas)
        {
            var orderedData = _rawData.OrderBy(x => x.TimeStamp);
            var firstEvent = orderedData.First();
            var lastEvent = orderedData.Last();

            var current = firstEvent.TimeStamp;
            int cnt = 0;

            while (current < lastEvent.TimeStamp)
            {
                Control c, label;

                if ((cnt % 10) == 0)
                {
                    c = CreateBigMarker();
                    label = CreateLabel($"{current:HH:mm:ss.fff}");
                }
                else
                {
                    c = CreateSmallMarker();
                    label = null;
                }

                Canvas.SetLeft(c, (DIAMETER) + LEFT_MARGIN + cnt * DISTANCE_X);
                canvas.Children.Add(c);

                if (label != null)
                {
                    Canvas.SetLeft(label, (DIAMETER) + LEFT_MARGIN + cnt * DISTANCE_X - 20);
                    Canvas.SetTop(label, 14);
                    canvas.Children.Add(label);
                }

                current = current.AddMilliseconds(1);
                cnt++;
            }
        }

        private Control CreateSmallMarker()
        {
            var border = new Border();
            border.Width = 2;
            border.Height = 8;
            border.BorderThickness = new Thickness(1);
            border.BorderBrush = Brushes.Black;

            return border;
        }

        private Control CreateBigMarker()
        {
            var border = new Border();
            border.Width = 2;
            border.Height = 12;
            border.BorderThickness = new Thickness(1);
            border.BorderBrush = Brushes.Black;

            return border;
        }

        private Control CreateLabel(string label)
        {
            var tb = new TextBlock();
            tb.FontSize = 8;
            tb.Text = label;

            return tb;
        }

        private double MsDiff(DateTime refTs, DateTime ts)
        {
            var ret = TimeSpan.FromTicks(ts.Ticks - refTs.Ticks).TotalMilliseconds;

            return ret;
        }

        private Ellipse CreateDot(ParsedObject parsedObject)
        {
            var ellipse = new Ellipse();
            ellipse.DataContext = parsedObject;
            ellipse.Width = DIAMETER;
            ellipse.Height = DIAMETER;
            ellipse.Stroke = Brushes.Black;
            ellipse.StrokeThickness = 1;

            switch (parsedObject.EventType)
            {
                case ParsedEventType.Measuring:
                    ellipse.Fill = Brushes.LightCoral;
                    break;

                case ParsedEventType.Arranging:
                    ellipse.Fill = Brushes.LightBlue;
                    break;

                default:
                    ellipse.Fill = Brushes.Gray;
                    break;
            }

            return ellipse;
        }

        private const double DIAMETER = 12; // radius of event circle
        private const double DISTANCE_X = 12; // distance between events per ms
        private const double MIN_DISTANCE_X = 12; // minimum distance b/w events
        private const double TRACK_Y = 30; // distance between tracks
        private const double TOP_MARGIN = 40; // top margin for canvas
        private const double LEFT_MARGIN = 300; // left margin for canvas
        private List<ParsedObject> _rawData;
    }
}
