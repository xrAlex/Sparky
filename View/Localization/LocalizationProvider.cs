using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace View.Localization
{
    [ContentProperty(nameof(LocalizationsDictionary))]
    public sealed class LocalizationProvider : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            throw new NotImplementedException();
        }

        public event EventHandler<string> LocalizationChanged = null!;
        public string CurrentLocalization
        {
            get => (string)GetValue(CurrentLocalizationProperty);
            set
            {
                SetValue(CurrentLocalizationProperty, value);
                LocalizationChanged?.Invoke(this, value);
            }
        }

        /// <summary>
        /// Коллекция словарей локализации.
        /// </summary>
        public Dictionary<object, LocalizationResource> LocalizationsDictionary
        {
            get => (Dictionary<object, LocalizationResource>)GetValue(LocalizationsDictionaryProperty);
            set => SetValue(LocalizationsDictionaryProperty, value);
        }

        /// <summary>
        /// Локализуемое приложение.
        /// </summary>
        public Application App
        {
            get => (Application)GetValue(AppProperty);
            set => SetValue(AppProperty, value);
        }

        public static readonly DependencyProperty CurrentLocalizationProperty =
            DependencyProperty.Register(
                nameof(CurrentLocalization),
                typeof(string),
                typeof(LocalizationProvider));

        /// <summary><see cref="DependencyProperty"/> для свойства <see cref="LocalizationsDictionary"/>.</summary>
        public static readonly DependencyProperty LocalizationsDictionaryProperty =
            DependencyProperty.Register(
                nameof(LocalizationsDictionary), 
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
                    (d, _) => ((LocalizationProvider)d).LocalizationChange()));

        public LocalizationProvider()
        {
            //var asd = IoC.GetInstance<IAppSettingsModel>().IsFullScreenAppCheckEnabled;
            LocalizationsDictionary = new Dictionary<object, LocalizationResource>();
            BindingOperations.SetBinding(this, AppProperty, new Binding());
        }

        public string GetLocalizedString(string key)
        {
            var localizationDict = LocalizationsDictionary[CurrentLocalization];
            return localizationDict.Contains(key) ? localizationDict[key].ToString()! : "ERROR";
        }

        private void LocalizationChange()
        {
            var app = App;
            if (app == null)
                return;
            var locs = LocalizationsDictionary;
            if (locs == null)
                return;
            var loc = CurrentLocalization;
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
                        break;
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
