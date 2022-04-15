using System;
using System.Globalization;
using System.Windows.Data;

namespace View.Converters;

[ValueConversion(typeof(double), typeof(string))]
internal sealed class BrightnessToPercent : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            var val = System.Convert.ToDouble(value);
            return $"{Math.Round(val * 100)}";
        }
        catch (Exception)
        {
            if (value == null)
                return "null";
            if (value is string str)
                str = $"\"{str}\"";
            else
                str = value.ToString();

            return $"{value.GetType().FullName}; {str}";
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}