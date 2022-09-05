using System;
using System.Collections.Generic;
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

namespace WpfApp6_ScrollViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var a = scrollViewer1;
            a.ScrollChanged += A_ScrollChanged;

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Timer_Tick;
            timer.Start();    
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            (sender as DispatcherTimer).Stop();

            var canvas = scrollViewer1.Content as Canvas;
            //scrollViewer1.ScrollToHorizontalOffset(20);
            canvas.Width *= 1.13777;
        }

        private void A_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {

        }
    }
}
