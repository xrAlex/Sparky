using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Controls;
using Common.DTO;
using Common.Entities;
using Common.Extensions.CollectionChanged;
using Common.Infrastructure;
using Common.Interfaces;
using Model.Entities;
using Model.Settings;
using WindowsDisplayAPI.DisplayConfig;

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
