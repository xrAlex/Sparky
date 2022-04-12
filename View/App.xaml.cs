using Common.Infrastructure;
using Common.Interfaces;
using Model;
using System;
using System.Reflection;
using System.Windows;
using System.Xml;
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

        var ru = GetLocalization("View.Localization.WithDto.RuLocalization.xml");
        Localizator.Instance.Localizations.Add(ru.Language, ru);
        Localizator.Instance.Default = ru;
        Localizator.Instance.Current = ru;

        new MainWindow().Show();

        base.OnStartup(e);
    }

    public static LocalizationDto? GetLocalization(string embeddedFileName)
    {
        using var resourceStream = Assembly
            .GetCallingAssembly()
            .GetManifestResourceStream(embeddedFileName);

        if (resourceStream == null)
            throw new ArgumentNullException(nameof(resourceStream));

        return LocalizationDto.ParseXml(XmlReader.Create(resourceStream));
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