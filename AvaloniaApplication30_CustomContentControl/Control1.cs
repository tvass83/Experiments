using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Markup.Xaml.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication30_CustomContentControl
{
    public class Control1 : ContentControl
    {
        public Control1()
        {
            ContentTemplate = new DataTemplate()
            {
                Content = new ContentPresenter()
            };

            Content = new TextBlock()
            {
                Text = "hello from ContentControl"
            };
        }
    }
}
