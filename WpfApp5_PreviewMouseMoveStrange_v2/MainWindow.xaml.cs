using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp5_PreviewMouseMoveStrange_v2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _slider.Value += _slider.SmallChange;
        }

        private void slider1_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Debug.WriteLine($"TV: {DateTime.Now}");
        }
    }
}
