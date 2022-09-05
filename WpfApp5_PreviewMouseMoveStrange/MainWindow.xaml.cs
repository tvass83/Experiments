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

namespace WpfApp5_PreviewMouseMoveStrange
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Debug.WriteLine($"TV: {DateTime.Now}");
        }

        private void MediaElement_Loaded(object sender, RoutedEventArgs e)
        {
            var mp = (MediaElement)sender;
            //mp.Play();
        }

        private void MediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {

        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {

        }
    }
}
