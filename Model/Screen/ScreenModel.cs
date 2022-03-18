using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Common.Interfaces;
using Model.Entities;
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

            _screenCollection.CollectionChanged += ScreenCollectionChanged;
            _appSettings.SettingsLoaded += SettingsLoaded;
            _appSettings.SettingsReset += SettingsReset;
        }

        private void SettingsReset(object? sender, System.EventArgs e) 
            => _screenCollection.Clear();

        private void SettingsLoaded(object? sender, System.EventArgs e) 
            => LoadScreens();
    }
}
