﻿using System;
using System.Threading.Tasks;

namespace Common.Interfaces;

public interface IAppSettingsModel
{
    /// <summary>
    /// Если  <see langword="true"/>, то приложение будет проверять другие
    /// запущенные приложения на наличие окна развернутого в полноэкранный режим
    /// </summary>
    bool IsFullScreenAppCheckEnabled { get; set; }

    /// <summary>
    /// Если  <see langword="true"/>, то гамма и яркость между периода будет менятся плавно
    /// </summary>
    bool IsGammaSmoothingEnabled { get; set; }

    /// <summary>
    /// Ключ локализации
    /// </summary>
    string? CurrentLocalizationKey { get; set; }

    /// <summary>
    /// Загружает настройки приложения из файла
    /// </summary>
    void Load();

    /// <summary>
    /// Асинхронно загружает настройки приложения из файла
    /// </summary>
    Task LoadAsync();

    /// <summary>
    /// Сохраняет настройки настройки приложения в файл
    /// </summary>
    void Save();

    /// <summary>
    /// Очищает данные и заново подгружает настройки приложения из файла
    /// </summary>
    void Reset();

    /// <summary>
    /// Асинхронно очищает данные и заново подгружает настройки приложения из файла
    /// </summary>
    Task ResetAsync();

    /// <summary>
    /// Асинхронно сохраняет настройки настройки приложения в файл
    /// </summary>
    Task SaveAsync();

    /// <summary>
    /// Событие сброса настроек
    /// </summary>
    event EventHandler? SettingsReset;

    /// <summary>
    /// Событие сохранения настроек
    /// </summary>
    event EventHandler? SettingsSaved;

    /// <summary>
    /// Событие загрузки настроек
    /// </summary>
    event EventHandler? SettingsLoaded;
}