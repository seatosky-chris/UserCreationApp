using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCreationUI.Utilities
{
    public class IfValueEqualsConverter : IValueConverter
    {
        HashSet<Type> NumericTypes = new HashSet<Type>
        {
            typeof(byte), typeof(sbyte),
            typeof(uint), typeof(int),
            typeof(short), typeof(ushort),
            typeof(long), typeof(ulong),
            typeof(decimal), typeof(double), typeof(float)
        };

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (parameter.GetType() == typeof(string) && NumericTypes.Contains(value.GetType()))
            {
                parameter = System.Convert.ChangeType(parameter, value.GetType());
            }

            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (parameter.GetType() == typeof(string) && NumericTypes.Contains(value.GetType()))
            {
                parameter = System.Convert.ChangeType(parameter, value.GetType());
            }

            return value.Equals(true) ? parameter : false;
        }
    }
}
