using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication54_CenteringBorderGoesAwry
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

            var p = this.Find<CalendarDatePicker>("datepicker");
            //p.SelectedDate = System.DateTime.Now;
        }
    }
}
