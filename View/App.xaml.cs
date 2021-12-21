using System;
using System.Reflection;
using System.Windows;
using Common.Infrastructure;
using Common.Interfaces;
using Model;
using Model.Screen;
using Model.Settings;
using SimpleInjector;

namespace View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
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

            container.Verify();
            _ = new IoCKernel(container);
        }
    }
}
