using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
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

namespace WpfApp8_Arrange
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Test();
        }

        public void Test()
        {
            var panel = new Border();
            var border = new Border();
            var textblock = new TextBlock();

            border.Child = textblock;
            panel.Child = border;

            const string pleaseWait = "Please Wait";
            textblock.Text = pleaseWait;
            panel.Visibility = Visibility.Visible;

            Size s = new Size(800, 600);
            panel.Measure(s);
            panel.Arrange(new Rect(0, 0, panel.DesiredSize.Width, panel.DesiredSize.Height));

            /* So far, so good */

            textblock.Text = "";
            panel.Visibility = Visibility.Hidden;

            panel.Measure(s);
            border.Measure(s);
            textblock.Measure(s);

            panel.Arrange(new Rect(0, 0, panel.DesiredSize.Width, panel.DesiredSize.Height));
            border.Arrange(new Rect(0, 0, panel.DesiredSize.Width, panel.DesiredSize.Height));
            textblock.Arrange(new Rect(0, 0, panel.DesiredSize.Width, panel.DesiredSize.Height));

            /* STEP 2 */

            textblock.Text = pleaseWait;
            panel.Visibility = Visibility.Visible;

            panel.Measure(s);
            border.Measure(s);
            textblock.Measure(s);

            panel.Arrange(new Rect(0, 0, panel.DesiredSize.Width, panel.DesiredSize.Height));
            //border.Arrange(new Rect(0, 0, panel.DesiredSize.Width, panel.DesiredSize.Height));
            //textblock.Arrange(new Rect(0, 0, panel.DesiredSize.Width, panel.DesiredSize.Height));

            var tbBounds = (textblock.ActualHeight, textblock.ActualWidth);

            panel.Arrange(new Rect(0, 0, panel.DesiredSize.Width, panel.DesiredSize.Height));
            tbBounds = (textblock.ActualHeight, textblock.ActualWidth);

            panel.Arrange(new Rect(0, 0, panel.DesiredSize.Width, panel.DesiredSize.Height));
            tbBounds = (textblock.ActualHeight, textblock.ActualWidth);
        }
    }
}
