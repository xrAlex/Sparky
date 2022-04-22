using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using View.Localization;

namespace View.Converters
{
    [ValueConversion(typeof(bool), typeof(string))]
    internal class LocalizationKeyToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var started = (bool) value;

            if (started)
            {
                return App.LocalizationProvider.GetLocalizedString("LocTrayUnPause");
            }

            return App.LocalizationProvider.GetLocalizedString("LocTrayPause");
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
