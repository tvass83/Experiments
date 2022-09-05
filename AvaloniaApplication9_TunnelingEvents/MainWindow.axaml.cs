using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using System;

namespace AvaloniaApplication9_TunnelingEvents
{
    public class MainWindow : Window
    {
        private bool _acceptLastKey = false;

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

            var tb = this.FindControl<TextBox>("textBox1");
            //tb.AddHandler(KeyDownEvent, Tb_KeyDown, Avalonia.Interactivity.RoutingStrategies.Tunnel);
            tb.AddHandler(TextInputEvent, Tb_TextInput, Avalonia.Interactivity.RoutingStrategies.Tunnel);
            //tb.TextInput += Tb_TextInput;

            tb.KeyDown += Tb_KeyDown;
        }

        private void Tb_TextInput(object? sender, TextInputEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"TV: #chars => {e.Text.Length}");

            if (!_acceptLastKey)
            {
                e.Handled = true;
                //(sender as TextBox).TextInput -= Tb_TextInput;
                (sender as TextBox).RemoveHandler(TextInputEvent, Tb_TextInput);
            }
        }

        private void Tb_KeyDown(object? sender, KeyEventArgs e)
        {
            if (!AcceptKey(e))
            {
                e.Handled = true;

                _acceptLastKey = false;
                //(sender as TextBox).TextInput += Tb_TextInput;
                (sender as TextBox).AddHandler(TextInputEvent, Tb_TextInput, Avalonia.Interactivity.RoutingStrategies.Tunnel);

                return;
            }

            _acceptLastKey = true;
        }

        private bool AcceptKey(KeyEventArgs e)
        {
            return
                e.Key == Key.Delete ||
                e.Key == Key.Back ||
                e.Key == Key.D0 ||
                e.Key == Key.D1 ||
                e.Key == Key.D2 ||
                e.Key == Key.D3 ||
                e.Key == Key.D4 ||
                e.Key == Key.D5 ||
                e.Key == Key.D6 ||
                e.Key == Key.D7 ||
                e.Key == Key.D8 ||
                e.Key == Key.D9 ||
                e.Key == Key.NumPad0 ||
                e.Key == Key.NumPad1 ||
                e.Key == Key.NumPad2 ||
                e.Key == Key.NumPad3 ||
                e.Key == Key.NumPad4 ||
                e.Key == Key.NumPad5 ||
                e.Key == Key.NumPad6 ||
                e.Key == Key.NumPad7 ||
                e.Key == Key.NumPad8 ||
                e.Key == Key.NumPad9 ||
                e.Key == Key.Tab ||
                e.Key == Key.Up ||
                e.Key == Key.Down ||
                e.Key == Key.Left ||
                e.Key == Key.Right ||
                e.Key == Key.Enter ||
                e.Key == Key.Escape;
        }
    }
}
