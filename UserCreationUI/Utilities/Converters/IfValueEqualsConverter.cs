using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCreationUI.Converters.Utilities
{
    public class IfValueEqualsConverter : IValueConverter
    {
        readonly HashSet<Type> NumericTypes = new()
        {
            typeof(byte), typeof(sbyte),
            typeof(uint), typeof(int),
            typeof(short), typeof(ushort),
            typeof(long), typeof(ulong),
            typeof(decimal), typeof(double), typeof(float)
        };

        public object? Convert(object? value, Type targetType, object? parameter,
            System.Globalization.CultureInfo culture)
        {
            if (parameter is not null && value is not null &&
                parameter.GetType() == typeof(string) && NumericTypes.Contains(value.GetType()))
            {
                parameter = System.Convert.ChangeType(parameter, value.GetType());
            }

            return Object.Equals(value, parameter);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter,
            System.Globalization.CultureInfo culture)
        {
            if (parameter is not null && value is not null && 
                parameter.GetType() == typeof(string) && NumericTypes.Contains(value.GetType()))
            {
                parameter = System.Convert.ChangeType(parameter, value.GetType());
            }

            return Object.Equals(value, true) ? parameter : false;
        }
    }
}
