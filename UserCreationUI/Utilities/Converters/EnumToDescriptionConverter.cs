using Avalonia.Data.Converters;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace UserCreationUI.Converters.Utilities
{
    public class EnumToDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            Type type = value.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("Value must be of Enum type", "value");
            }

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            var name = Enum.GetName(type, value);
            var enumMemberAttributes = ((EnumMemberAttribute[])type.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
            return enumMemberAttributes.Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
