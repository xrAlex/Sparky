using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace View.Localization
{
    public class LocalizationProvider : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            throw new NotImplementedException();
        }

        public static void SetLocalizationDictionary()
        {

        }

        public ResourceDictionary Localization
        {
            get => (ResourceDictionary)GetValue(LocalizationDictionary);
            set => SetValue(LocalizationDictionary, value);
        }

        public static readonly DependencyProperty LocalizationDictionary =
            DependencyProperty.Register(
                nameof(Localization),
                typeof(ResourceDictionary),
                typeof(LocalizationProvider));


        public LocalizationProvider()
        {

        }

        public static string GetLocalizedString(string param)
        {

            return "Localization error";
        }
    }
}
