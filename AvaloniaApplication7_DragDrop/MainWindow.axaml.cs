using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication7_DragDrop
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
            AvaloniaXamlLoader.Load(this); //new Button().PointerPressed
        }

        private async void Grid_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            //if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                var data = new DataObject();
                //data.Set(typeof(string).FullName, "hi");
                data.Set(DataFormats.Text, "hello");
                data.Set("OEMText", "hwllo");

                // Inititate the drag-and-drop operation.
                var res = await DragDrop.DoDragDrop(e, data, DragDropEffects.Copy | DragDropEffects.Link | DragDropEffects.Move);
                System.Diagnostics.Debug.WriteLine($"TV: result => {res}");
            }
        }

        private async void Grid_PointerMoved(object sender, PointerEventArgs e)
        {
            //if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            //{
            //    var data = new DataObject();
            //    //data.Set(typeof(string).FullName, "hi");
            //    data.Set(DataFormats.Text, "hello");

            //    // Inititate the drag-and-drop operation.
            //    var res = await DragDrop.DoDragDrop(e, data, DragDropEffects.Copy);
            //    System.Diagnostics.Debug.WriteLine($"TV: result => {res}");
            //}
        }
    }
}
