using Avalonia;
using Avalonia.Controls;
using System;

namespace AvaloniaApplication14_PropertyChangedCallback
{
    public class Control1 : Control
    {
        static Control1()
        {
            //FooProperty.Changed.Subscribe(TipChanged);
            FooProperty.Changed.Subscribe(TipChanged);
            FooProperty.Changed.AddClassHandler<Control1>(FooChanged);
        }

        private static void TipChanged(AvaloniaPropertyChangedEventArgs<int> e) 
        {
        
        }

        private static void FooChanged(Control1 c, AvaloniaPropertyChangedEventArgs e)
        {
            
        }

        public int Foo
        {
            get { return GetValue(FooProperty); }
            set { SetValue(FooProperty, value); }
        }

        public static readonly StyledProperty<int> FooProperty = AvaloniaProperty.Register<Control1, int>(nameof(Foo), defaultValue: -1);
    }
}
