using System;
using System.Collections.Generic;
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

        public object Localization
        {
            get => GetValue(LocalizationDictionary);
            set => SetValue(LocalizationDictionary, value);
        }

        /// <summary>
        /// Коллекция словарей локализации.
        /// </summary>
        public Dictionary<object, LocalizationResource> Localizations
        {
            get => (Dictionary<object, LocalizationResource>)GetValue(LocalizationsProperty);
            set => SetValue(LocalizationsProperty, value);
        }

        /// <summary>
        /// Локализуемое приложение.
        /// </summary>
        public Application App
        {
            get => (Application)GetValue(AppProperty);
            set => SetValue(AppProperty, value);
        }

        public static readonly DependencyProperty LocalizationDictionary =
            DependencyProperty.Register(
                nameof(Localization),
                typeof(object),
                typeof(LocalizationProvider));

        /// <summary><see cref="DependencyProperty"/> для свойства <see cref="Localizations"/>.</summary>
        public static readonly DependencyProperty LocalizationsProperty =
            DependencyProperty.Register(
                nameof(Localizations), 
                typeof(Dictionary<object, LocalizationResource>),
                typeof(LocalizationProvider),
                new PropertyMetadata(null,
                    (d, _) => ((LocalizationProvider)d).LocalizationChange()));

        /// <summary><see cref="DependencyProperty"/> для свойства <see cref="App"/>.</summary>
        public static readonly DependencyProperty AppProperty =
            DependencyProperty.Register(
                nameof(App),
                typeof(Application),
                typeof(LocalizationProvider),
                new PropertyMetadata(null,
                    (d, _e) => ((LocalizationProvider)d).LocalizationChange()));

        public LocalizationProvider()
        {
            Localizations = new Dictionary<object, LocalizationResource>();
        }

        public string GetLocalizedString(string param)
        {
            var localizationDict = Localizations["Rus"];
            var localizedString = localizationDict[param].ToString();
            return localizedString;
        }

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
            else
            {
                // Действия на случай если с таким ключом ресур не найден.
                // Можно добавить свойство для локализации по умолчанию и устанавливать её.
            }
        }

    }

    public class LocalizationResource : ResourceDictionary { }
}
