using Common.Infrastructure;
using Common.Interfaces;
using Model;
using System;
using System.Windows;
using View.Localization.WithDto;
using View.Views;
using ViewModel;

namespace View;

public partial class App : Application
{
    private static readonly string ConfigurationFilepath
        = $"{Environment.CurrentDirectory}" + "\\Settings.json";
    
    private IAppSettingsModel? _settings;
    private IPeriodObserverModel? _observer;
}

public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

        ConfigureIoC();

        _settings = IoC.GetInstance<IAppSettingsModel>();
        _observer = IoC.GetInstance<IPeriodObserverModel>();

        _settings.Load();
        _observer.RefreshAllScreensColorConfiguration();
        _observer.StartWatch();

        Localizator.Instance.Default = LocalizationDto.ParseEmbeddedXml("View.Localization.WithDto.RuLocalization.xml");
        Localizator.Instance.Current = LocalizationDto.ParseEmbeddedXml("View.Localization.WithDto.RuLocalization.xml");

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