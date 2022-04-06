using Common.Infrastructure;
using Common.Interfaces;
using Model;
using System;
using System.Windows;
using View.Localization;
using View.Views;
using ViewModel;

namespace View
{
    public partial class App : Application
    {
        private static readonly string ConfigurationFilepath
            = $"{Environment.CurrentDirectory}" + "\\Settings.json";

        public static LocalizationProvider LocalizationProvider { get; private set; } = null!;
        private IAppSettingsModel? _settings;
        private IPeriodObserverModel? _observer;

        private void OnStartup(object sender, StartupEventArgs e)
        {
            ConfigureIoC();
            _settings = IoC.GetInstance<IAppSettingsModel>();
            _observer = IoC.GetInstance<IPeriodObserverModel>();

            _settings.Load();
            _observer.RefreshAllScreensColorConfiguration();
            _observer.StartWatch();

            LocalizationProvider = FindResource(nameof(LocalizationProvider)) as LocalizationProvider 
                                   ?? throw new InvalidOperationException("Localization provider not founded");
            LocalizationProvider.App = this;
            LocalizationProvider.CurrentLocalization = _settings.CurrentLocalizationKey;

            LocalizationProvider.LocalizationChanged += (_, value) =>
            {
                _settings.CurrentLocalizationKey = value;
            };

            new MainWindow().Show();
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
}
