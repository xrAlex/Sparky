using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace View.Converters
{
    [ValueConversion(typeof(byte), typeof(string))]
    internal sealed class HourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hour = (byte)value;
            var result = System.Convert.ToString(hour);

            if (result.Length < 2)
            {
                result = $"0{result}";
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valid = byte.TryParse((string)value, out var hour);

            if (!valid)
            {
                return DependencyProperty.UnsetValue;
            }

            if (hour is > 23 or < 0) hour = 23;

            return hour;
        }
    }
}