using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace View.Converters;

internal class ObserverStatusToImage : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var status = (bool)value;
        var flagsPath = "pack://application:,,,/Resources/Images/";

        if (status)
        {
            flagsPath += "pause.png";
        }
        else
        {
            flagsPath += "play-button.png";
        }


        return new BitmapImage(new Uri(flagsPath));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}