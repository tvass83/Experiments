using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Layout.Visualizer.Model
{
    public enum ParsedEventType
    {
        Unknown,
        IsMeasureValid_set,
        IsArrangeValid_set,
        Bounds_set,
        DesiredSize_set,
        InvalidateMeasure,
        InvalidateArrange,
        ChildDesiredSizeChanged,
        Measuring,
        Arranging
    }
}
