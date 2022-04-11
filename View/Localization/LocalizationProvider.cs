using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using View.Localization.LangDictionaries;

namespace View.Localization;

[ContentProperty(nameof(Localizations))]
public sealed class LocalizationProvider : Freezable
{
    protected override Freezable CreateInstanceCore()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Событие уведомляющее о смене локализации.
    /// </summary>
    public event EventHandler<object?>? LocalizationChanged;

    /// <summary>
    /// Текущий ключ словаря локализации.
    /// </summary>
    public object CurrentLocalization
    {
        get => GetValue(CurrentLocalizationProperty);
        set => SetValue(CurrentLocalizationProperty, value);
    }

    /// <summary>
    /// Коллекция словарей локализации.
    /// </summary>
    public Dictionary<object, LocalizationDictionary> Localizations
    {
        get => (Dictionary<object, LocalizationDictionary>)GetValue(LocalizationsDictionaryProperty);
        set => SetValue(LocalizationsDictionaryProperty, value);
    }

    // <summary>
    // Локализуемое приложение.
    // </summary>
    //public Application App
    //{
    //    get => (Application)GetValue(AppProperty);
    //    set => SetValue(AppProperty, value);
    //}


    /// <summary>
    /// Текущий словарь локализации.
    /// </summary>
    public LocalizationDictionary CurrentResources
    {
        get => (LocalizationDictionary)GetValue(CurrentResourcesProperty);
        private set => SetValue(CurrentResourcesPropertyKey, value);
    }

    private static readonly DependencyPropertyKey CurrentResourcesPropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(CurrentResources),
            typeof(LocalizationDictionary),
            typeof(LocalizationProvider),
            new PropertyMetadata(null));
    

    public static readonly DependencyProperty CurrentResourcesProperty =
        CurrentResourcesPropertyKey.DependencyProperty;

    public static readonly DependencyProperty CurrentLocalizationProperty =
        DependencyProperty.Register(
            nameof(CurrentLocalization),
            typeof(object),
            typeof(LocalizationProvider),
            new PropertyMetadata(null,
                (d, _) => ((LocalizationProvider)d).LocalizationChange()));

    public static readonly DependencyProperty LocalizationsDictionaryProperty =
        DependencyProperty.Register(
            nameof(Localizations),
            typeof(Dictionary<object, LocalizationDictionary>),
            typeof(LocalizationProvider),
            new PropertyMetadata(null,
                (d, _) => ((LocalizationProvider)d).LocalizationChange()));
 
    //public static readonly DependencyProperty AppProperty =
    //    DependencyProperty.Register(
    //        nameof(App),
    //        typeof(Application),
    //        typeof(LocalizationProvider),
    //        new PropertyMetadata(null,
    //            (d, _) => ((LocalizationProvider)d).LocalizationChange()));

    public LocalizationProvider()
    {
        Localizations = new Dictionary<object, LocalizationDictionary>()
        {
            {"Rus", new RusDict().Localization},
            {"Eng", new EngDict().Localization},
        };
    }

    public string GetLocalizedString(string key)
    {
        var localizationDict = Localizations[CurrentLocalization];

        return localizationDict.ContainsKey(key) ? localizationDict[key] : "ERROR";
    }

    private void LocalizationChange()
    {
        //var app = App;
        //if (app == null)
        //    return;

        var locs = Localizations;
        if (locs == null)
            return;
        var loc = CurrentLocalization;
        if (loc == null)
            return;

        if (locs.TryGetValue(loc, out var localization))
        {

            //int i = 0;
            //for (; i < app.Resources.MergedDictionaries.Count; i++)
            //{
            //    if (app.Resources.MergedDictionaries[i] is LocalizationResource)
            //    {
            //        app.Resources.MergedDictionaries[i] = localization;
            //        LocalizationChanged?.Invoke(this, loc.ToString());
            //        break;
            //    }
            //}
            //if (i >= app.Resources.MergedDictionaries.Count)
            //{
            //    app.Resources.MergedDictionaries.Add(localization);
            //}

            CurrentResources = localization;
            LocalizationChanged?.Invoke(this, loc.ToString());
        }
        else
        {
            // Действия на случай если с таким ключом ресур не найден.
            // Можно добавить свойство для локализации по умолчанию и устанавливать её.
        }
    }

}

public class LocalizationResource : ResourceDictionary { }

