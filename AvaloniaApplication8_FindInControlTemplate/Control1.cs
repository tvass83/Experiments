using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication8_FindInControlTemplate
{
    public class Control1 : Slider
    {
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            var grid1 = this.FindControl<Grid>("grid1");
            var grid2 = this.Find<Grid>("grid1");
            var grid3 = e.NameScope.Find<Grid>("grid1");

            grid3.Background = Brushes.Red;
        }
    }
}
