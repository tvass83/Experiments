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

namespace WpfApp1_DispatcherTimer_On_BGThread
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Task.Run(() =>
            {
                var timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(500);
                timer.Tick += Timer_Tick;

                timer.Start();
            });

            //var timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromMilliseconds(500);
            //timer.Tick += Timer_Tick;

            //timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            (sender as DispatcherTimer).Stop();
            Debug.WriteLine($"TV: {DateTime.Now:HH:mm:ss.fff}");
        }
    }
}
