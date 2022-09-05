using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;

namespace AvaloniaApplication43_SelectedItem
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

            var cb = this.Find<ComboBox>("combobox1");
            //cb.SelectedItem = "bbb";
            //cb.Items = CreateItems();


            var btn = this.Find<Button>("btn");
            //btn.PointerPressed += PressedHandler;
            //btn.PointerPressed += Btn_PointerPressed;
        }
    }
}
