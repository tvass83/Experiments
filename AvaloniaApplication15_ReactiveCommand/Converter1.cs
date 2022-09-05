using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace AvaloniaApplication15_ReactiveCommand
{
    public class Converter1 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 5;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
