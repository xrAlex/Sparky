﻿using Common.Infrastructure;
using Common.Interfaces;
using Model;
using System;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using View.Localization;
using View.Views;
using ViewModel;

namespace View;

public partial class App : Application
{
    private static readonly string ConfigurationFilepath
        = $"{Environment.CurrentDirectory}" + "\\Settings.json";

    public static LocalizationProvider LocalizationProvider { get; private set; } = null!;
    public static TaskbarIcon? TaskBarIcon { get; private set; }

    private IAppSettingsModel? _settings;
    private IPeriodObserverModel? _observer;
}

public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

        ConfigureIoC();

        TaskBarIcon = FindResource(nameof(TaskBarIcon)) as TaskbarIcon 
                      ?? throw new InvalidOperationException("Task bar icon not founded");
        TaskBarIcon.NoLeftClickDelay = true;

        _settings = IoC.GetInstance<IAppSettingsModel>();
        _observer = IoC.GetInstance<IPeriodObserverModel>();

        _settings.Load();
        _observer.RefreshAllScreensColorConfiguration();
        _observer.StartWatch();
        
        LocalizationProvider = FindResource(nameof(LocalizationProvider)) as LocalizationProvider
                               ?? throw new InvalidOperationException("Localization provider not founded");
        LocalizationProvider.App = this;
        LocalizationProvider.CurrentLocalization = _settings.CurrentLocalizationKey!;

        LocalizationProvider.LocalizationChanged += (_, value) =>
        {
            _settings.CurrentLocalizationKey = value?.ToString();
        };

        new MainWindow().Show();

        base.OnStartup(e);
    }
}

public partial class App
{ 
    protected override void OnExit(ExitEventArgs e)
    {
        _observer?.StopWatch();
        _observer?.ForceDefaultColorConfiguration();

        if (TaskBarIcon != null)
        {
            TaskBarIcon.Visibility = Visibility.Collapsed;
            TaskBarIcon.Dispose();
        }

        base.OnExit(e);
    }
}

public partial class App
{
    /// <summary>
    /// Конфигурация контейнера инверсии управления
    /// </summary>
    private static void ConfigureIoC()
    {
        var container = IoC.Instance;
        ModelRegistrator.Register(container, ConfigurationFilepath);
        ViewModelRegistrator.Register(container);
    }
}