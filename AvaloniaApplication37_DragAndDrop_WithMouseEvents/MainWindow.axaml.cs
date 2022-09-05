using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication37_DragAndDrop
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

            _datagrid = this.FindControl<DataGrid>("_datagrid");

            var behav = new TestDragDrop(_datagrid);
            behav.DragCue = this.FindControl<Rectangle>("_dragCue");
            behav.Attach();
        }

        private DataGrid _datagrid;
    }
}
