using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCreationUI.Converters.Utilities
{
    /// <summary>
    /// A value converter which can convert multiple radio buttons into relative int values />
    /// </summary>
    public class RadioBoolToIntConverter : IValueConverter
    {
        #region IValueConverter Members
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is null)
                throw new ArgumentException("Value must be set", nameof(value));

            System.Diagnostics.Debug.WriteLine("Value: " + value);
            System.Diagnostics.Debug.WriteLine("TargetType: " + targetType);
            System.Diagnostics.Debug.WriteLine("Parameter: " + parameter);
            int integer = (int)value;
            if (parameter is null)
                return false;
            string? paramString = parameter.ToString();

            if (paramString is not null && integer == int.Parse(paramString))
                return true;
            else
                return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return parameter;
        }
        #endregion
    }
}
