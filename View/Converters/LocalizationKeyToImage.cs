using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace View.Converters;

[ValueConversion(typeof(string), typeof(BitmapImage))]
internal sealed class LocalizationKeyToImage : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var key = value.ToString();
        var flagsPath = "pack://application:,,,/Resources/Images/Flags/";

        switch (key)
        {
            case "Rus":
                flagsPath += "russia.png";
                break;
            case "Eng":
                flagsPath += "united-states.png";
                break;
            default:
                flagsPath += "russia.png";
                break;
        }

        return new BitmapImage(new Uri(flagsPath));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {                                                                                                                                                                                                                             
        throw new NotImplementedException();
    }
}