﻿using Common.Infrastructure;
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

        public static LocalizationProvider LocalizationProvider { get; private set; }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            LocalizationProvider = (LocalizationProvider) FindResource(nameof(LocalizationProvider));
            LocalizationProvider.App = this;

            ConfigureIoC();

            IoC.GetInstance<IAppSettingsModel>().Load();
            IoC.GetInstance<IPeriodObserverModel>().RefreshAllScreensColorConfiguration();
            IoC.GetInstance<IPeriodObserverModel>().StartWatch();
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
