using Avalonia.Data.Converters;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace UserCreationUI.Converters.Utilities
{
    public class EnumToDescriptionConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value is null)
                throw new ArgumentException("Value must be set", nameof(value));

            Type type = value.GetType();
            if (!type.IsEnum)
                throw new ArgumentException("Value must be of Enum type", nameof(value));

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            var name = Enum.GetName(type, value);
            if (name is null)
                throw new ArgumentException("Could not get the name of the constant of the value Enum. Is value a valid Enum?", nameof(value));

            var field = type.GetField(name);
            if (field is null)
                throw new ArgumentException("Could not get the public field of the value Enum. Is value a valid Enum?", nameof(value));

            var enumMemberAttributes = ((EnumMemberAttribute[])field.GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
            return enumMemberAttributes.Value;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter,
            System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
