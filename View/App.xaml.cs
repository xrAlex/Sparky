using System;
using System.Windows;
using Common.Infrastructure;
using Model;
using Model.Settings;
using SimpleInjector;
using ViewModel;

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

            //Загрузка настроек из файла
            var settings = IoCKernel.IoC.GetInstance<IAppSettingsModel>();
            settings.Load();
        }
    }

    public partial class App
    {
        /// <summary>
        /// Конфигурация контейнера инверсии управления
        /// </summary>
        private void ConfigureIoC()
        {
            var container = new Container();
            ModelRegistrator.Register(container, ConfigurationFilepath);
            ViewModelRegistrator.Register(container);

            container.Verify();
            _ = new IoCKernel(container);
        }
    }
}
