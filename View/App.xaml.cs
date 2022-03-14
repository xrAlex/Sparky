using System;
using System.Windows;
using Common.Infrastructure;
using Common.Interfaces;
using Model;
using Model.Settings;
using SimpleInjector;
using View.Views;
using ViewModel;
using ViewModel.ViewModels;

namespace View
{
    public partial class App : Application
    {
        private static readonly string ConfigurationFilepath 
            = $"{Environment.CurrentDirectory}" + "\\Settings.json";

        public App()
        {
            ConfigureIoC();
            InitializeComponent();

            IoC.GetInstance<IAppSettingsModel>().Load();
            var asd = new MainWindow();
            asd.Show();
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

            container.Verify();
        }
    }
}
