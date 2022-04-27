using Common.Infrastructure;
using Common.Interfaces;
using Model;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using View.Localization;
using View.Views;
using ViewModel;

namespace View;

public partial class App : Application
{
    private static readonly string ConfigurationFilepath
        = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}" + "\\Settings.json";

    private Mutex? _mutex;
    public static LocalizationProvider LocalizationProvider { get; private set; } = null!;
    public static TaskbarIcon TaskBarIcon { get; private set; } = null!;

    private IAppSettingsModel? _settings;
    private IPeriodObserverModel? _observer;
}

public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        _mutex = new Mutex(true, ResourceAssembly.GetName().Name);

        if (!_mutex.WaitOne())
        {
            Current.Shutdown();
        }

        Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

        ConfigureIoC();

        _settings = IoC.GetInstance<IAppSettingsModel>();
        _observer = IoC.GetInstance<IPeriodObserverModel>();

        _settings.Load();

        IoC.GetInstance<IRegistryModel>().ValidateStartupPath();

        ConfigureTaskBarIcon();
        ConfigureLocalizationProvider();

        var silentLaunch = Environment.GetCommandLineArgs().Contains("-silent");

        if (silentLaunch)
        {
            TaskBarIcon.Visibility = Visibility.Visible;
        }
        else
        {
            new MainWindow().Show();
        }

        _observer.RefreshAllScreensColorConfiguration();
        _observer.StartWatch();

        base.OnStartup(e);
    }
}

public partial class App
{
    protected override void OnExit(ExitEventArgs e)
    {
        _observer?.StopWatch();
        _observer?.ForceDefaultColorConfiguration();
        _mutex?.ReleaseMutex();
        TaskBarIcon.Visibility = Visibility.Collapsed;
        TaskBarIcon.Dispose();

        base.OnExit(e);
    }
}

public partial class App
{
    private void ConfigureTaskBarIcon()
    {
        TaskBarIcon = FindResource(nameof(TaskBarIcon)) as TaskbarIcon
                      ?? throw new InvalidOperationException("Task bar icon not founded");
        TaskBarIcon.NoLeftClickDelay = true;
    }

    private void ConfigureLocalizationProvider()
    {
        LocalizationProvider = FindResource(nameof(LocalizationProvider)) as LocalizationProvider
                               ?? throw new InvalidOperationException("Localization provider not founded");
        LocalizationProvider.App = this;
        LocalizationProvider.CurrentLocalization = _settings!.CurrentLocalizationKey ?? "Rus";

        LocalizationProvider.LocalizationChanged += (_, value) =>
        {
            _settings.CurrentLocalizationKey = value?.ToString();
        };
    }

    /// <summary>
    /// Конфигурация контейнера инверсии управления
    /// </summary>
    private static void ConfigureIoC()
    {
        var container = IoC.Instance;
        container.Options.EnableAutoVerification = false;

        ModelRegistrator.Register(container, ConfigurationFilepath);
        ViewModelRegistrator.Register(container);
    }
}