using Avalonia.Data;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace UserCreationUI.Converters.Utilities
{
    public class RadioButtonCheckedConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter,
            System.Globalization.CultureInfo culture)
        {
            return Object.Equals(value, parameter);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter,
            System.Globalization.CultureInfo culture)
        {
            return Object.Equals(value, true) ? parameter : false;
        }
    }
}
