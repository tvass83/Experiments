using Avalonia.Controls.Shapes;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml.Templates;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace AvaloniaApplication24_DictionaryConverter
{
    public class Converter1 :  IValueConverter
    {
        public Dictionary<SyncState, DataTemplate> SyncStatePaths { get; set; } = new Dictionary<SyncState, DataTemplate>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SyncState state)
            {
                return SyncStatePaths[state];
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
