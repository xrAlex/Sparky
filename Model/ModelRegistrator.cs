using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces;
using Model.Screen;
using Model.Settings;
using SimpleInjector;

namespace Model
{
    public sealed class ModelRegistrator
    {
        public static void Register(Container container, string configPath)
        {
            container.Register<IAppSettingsModel>(() => new AppSettingsModel(configPath), Lifestyle.Singleton);
            container.Register<IScreenModel, ScreenModel>(Lifestyle.Singleton);
        }
    }
}
