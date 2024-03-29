﻿using Common.Interfaces;
using Model.Settings;

namespace Model.Screen;

/// <summary>
/// Модель устройств отображения
/// </summary>
internal sealed partial class ScreenModel : IScreenModel
{
    private readonly ScreenCollection.ScreenCollection _screenCollection = new();
    private readonly AppSettingsModel _appSettings;
    private readonly object _eventLocker = new();

    public ScreenModel(IAppSettingsModel appSettings)
    {
        _appSettings = (AppSettingsModel)appSettings;

        _screenCollection.CollectionChanged += ScreenCollectionChanged;
        _appSettings.SettingsLoaded += SettingsLoaded;
        _appSettings.SettingsReset += SettingsReset;

        if (_appSettings.Loaded)
        {
            LoadScreens();
        }
    }

    private void SettingsReset(object? sender, System.EventArgs e) 
        => _screenCollection.Clear();

    private void SettingsLoaded(object? sender, System.EventArgs e) 
        => LoadScreens();
}