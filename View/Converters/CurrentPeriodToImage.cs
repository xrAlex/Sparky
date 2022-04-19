using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Common.Enums;

namespace View.Converters;

[ValueConversion(typeof(CurrentPeriod), typeof(BitmapImage))]
internal sealed class CurrentPeriodToImage : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var period = (CurrentPeriod)value;
        var imagePath = "pack://application:,,,/Resources/Images/";

        switch (period)
        {
            case CurrentPeriod.Day:
                imagePath += "DayImg.png";
                break;
            case CurrentPeriod.Night:
                imagePath += "NightImg.png";
                break;
            case CurrentPeriod.Transition:
                imagePath += "refresh-option.png";
                break;
            default:
                imagePath += "DayImg.png";
                break;
        }

        return new BitmapImage(new Uri(imagePath));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}