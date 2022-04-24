using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace View.Converters;

[ValueConversion(typeof(bool), typeof(BitmapImage))]
internal sealed class ObserverStatusToImage : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var started = (bool)value;
        var flagsPath = "pack://application:,,,/Resources/Images/";

        if (parameter != null && parameter.ToString() == "Black")
        {
            if (started)
            {
                flagsPath += "pauseBlack.png";
            }
            else
            {
                flagsPath += "play-buttonBlack.png";
            }
        }
        else
        {
            if (started)
            {
                flagsPath += "pause.png";
            }
            else
            {
                flagsPath += "play-button.png";
            }
        }


        return new BitmapImage(new Uri(flagsPath));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}