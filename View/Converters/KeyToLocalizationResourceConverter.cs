using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace View.Localization;

public sealed class KeyToLocalizationResourceConverter : IMultiValueConverter, IValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            var resource = (LocalizationResource)values[0];
            return resource[values[1]];
        }
        catch (Exception)
        {
            // ignored
        }

        return DependencyProperty.UnsetValue;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            var resource = (LocalizationResource)value;
            return resource[parameter];
        }
        catch (Exception)
        {
            // ignored
        }

        return DependencyProperty.UnsetValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    public static KeyToLocalizationResourceConverter Instance { get; } = new();
}



