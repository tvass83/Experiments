using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication31_ItemPresenter_ItemsBindingConverter.Converters
{
    public class FilterInfoEventsConverter : IValueConverter
    {
        private static HashSet<string> BlackListedFields = new HashSet<string>
        {
            "aaa"
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var fields = value as List<string>;
            if (fields != null)
            {
                return fields.Where(x => !BlackListedFields.Contains(x));
            }

            return AvaloniaProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
