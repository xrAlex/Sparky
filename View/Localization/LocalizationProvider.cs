using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace View.Localization
{
    [ContentProperty(nameof(Localizations))]
    public class LocalizationProvider : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            throw new NotImplementedException();
        }

        public static void SetLocalizationDictionary()
        {

        }

        public object Localization
        {
            get => GetValue(LocalizationDictionary);
            set => SetValue(LocalizationDictionary, value);
        }

        public static readonly DependencyProperty LocalizationDictionary =
            DependencyProperty.Register(
                nameof(Localization),
                typeof(object),
                typeof(LocalizationProvider));


        public LocalizationProvider()
        {
            Localizations = new Dictionary<object, LocalizationResource>();
        }

        public static string GetLocalizedString(string param)
        {

            return "Localization error";
        }


        /// <summary>
        /// Коллекция словарей локализации.
        /// </summary>
        public Dictionary<object, LocalizationResource> Localizations
        {
            get { return (Dictionary<object, LocalizationResource>)GetValue(LocalizationsProperty); }
            set { SetValue(LocalizationsProperty, value); }
        }

        /// <summary><see cref="DependencyProperty"/> для свойства <see cref="Localizations"/>.</summary>
        public static readonly DependencyProperty LocalizationsProperty =
            DependencyProperty.Register(nameof(Localizations), typeof(Dictionary<object, LocalizationResource>), typeof(LocalizationProvider),
                new PropertyMetadata(null, (d, e) => ((LocalizationProvider)d).LocalizationChange()));



        /// <summary>
        /// Локализуемое приложение.
        /// </summary>
        public Application App
        {
            get { return (Application)GetValue(AppProperty); }
            set { SetValue(AppProperty, value); }
        }

        /// <summary><see cref="DependencyProperty"/> для свойства <see cref="App"/>.</summary>
        public static readonly DependencyProperty AppProperty =
            DependencyProperty.Register(nameof(App), typeof(Application), typeof(LocalizationProvider),
                new PropertyMetadata(null, (d, e) => ((LocalizationProvider)d).LocalizationChange()));


        private void LocalizationChange()
        {
            var app = App;
            if (app == null)
                return;
            var locs = Localizations;
            if (locs == null)
                return;
            var loc = Localization;
            if (loc == null)
                return;

            if (locs.TryGetValue(loc, out var localization))
            {
                int i = 0;
                for (; i < app.Resources.MergedDictionaries.Count; i++)
                {
                    if (app.Resources.MergedDictionaries[i] is LocalizationResource)
                    {
                        app.Resources.MergedDictionaries[i] = localization;
                    }
                }
                if (i >= app.Resources.MergedDictionaries.Count)
                {
                    app.Resources.MergedDictionaries.Add(localization);
                }
            }
        }

    }

    public class LocalizationResource : ResourceDictionary { }
}
