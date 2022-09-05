using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication17_Binding_TargetNullValue
{
    public class Converter1 : IValueConverter, IMultiValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine($"TV: type of value => {value.GetType()}");

            return Brushes.Red;
        }

        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (var item in values)
            {
                Debug.WriteLine($"TV: type of value => {(item != null ? item.GetType().ToString() : "null")}");
            }

            return Brushes.Green;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
