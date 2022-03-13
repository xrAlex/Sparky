using System;
using System.Threading.Tasks;
using System.Windows;
using Common.Interfaces;
using Model.Settings;

namespace Model.Screen
{
    internal sealed partial class ScreenModel : IScreenModel
    {
        private readonly ScreenCollection.ScreenCollection _screenCollection = new();

        private readonly AppSettingsModel _appSettings;

        private readonly object _eventLocker = new();

        public ScreenModel(IAppSettingsModel appSettings)
        {
            _appSettings = (AppSettingsModel)appSettings;
            _screenCollection.CollectionChanged += CollectionChanged;

            Task.Run(LoadScreens).ConfigureAwait(false);
        }
    }
}
