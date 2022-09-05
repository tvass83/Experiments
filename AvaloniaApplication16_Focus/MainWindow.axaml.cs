using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using System;

namespace AvaloniaApplication16_Focus
{
    public class MainWindow : Window
    {
        private TextBox _tb;

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

            _tb = this.FindControl<TextBox>("textBox1");
            _tb.AddHandler(PointerPressedEvent, SelectivelyIgnoreMouseButton, RoutingStrategies.Tunnel);
            _tb.GotFocus += Tb_GotFocus;
            var a = _tb.GetObservable(InputElement.IsKeyboardFocusWithinProperty)
                       .Subscribe(KeyboardFocus_ValueChanged);
        }

        private void KeyboardFocus_ValueChanged(bool newValue)
        {
            
        }

        private void Tb_GotFocus(object sender, Avalonia.Input.GotFocusEventArgs e)
        {
            _tb.SelectAll();
        }

        private static void SelectivelyIgnoreMouseButton(object sender,
                                                         PointerPressedEventArgs e)
        {
            //var parent = e.Source as IVisual;
            //while (parent != null && !(parent is TextBox))
            //{
            //    parent = parent.GetVisualParent();
            //}

            //if (parent != null)
            //{
            //    var textBox = (TextBox)parent;
            //    if (!textBox.IsKeyboardFocusWithin)
            //    {
            //        textBox.Focus();
            //        e.Handled = true;
            //    }
            //}

            var textBox = (TextBox)sender;
            if (!textBox.IsKeyboardFocusWithin)
            {
                textBox.Focus();
                e.Handled = true;
            }
        }
    }
}
