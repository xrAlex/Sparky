using System;
using Common.DTO;
using Common.Entities;
using Common.Extensions.CollectionChanged;
using Common.Infrastructure;
using Common.Interfaces;
using Model.Settings;

namespace Model.Screen
{
    public sealed partial class ScreenModel : IScreenModel
    {
        private readonly ScreenCollection.ScreenCollection _screenCollection = new();

        public event EventHandler<ScreensCollectionChangedArgs>? ScreensCollectionChanged;

        private readonly AppSettingsModel _appSettings;

        public ScreenModel(AppSettingsModel appSettings)
        {
            _appSettings = appSettings;
            _screenCollection.CollectionChanged += CollectionChanged;
            LoadSettings();
        }

        /// <summary>
        /// Загружает настройки устройств отображения
        /// </summary>
        private void LoadSettings()
        {
            foreach (var screenCode in _appSettings.Screens.Keys)
            {
                if (_screenCollection.TryGetValue(screenCode, out var screen))
                {
                    _screenCollection[screenCode].NightColorConfiguration = screen.NightColorConfiguration;
                    _screenCollection[screenCode].DayColorConfiguration = screen.DayColorConfiguration;
                    _screenCollection[screenCode].DayStartTime = screen.DayStartTime;
                    _screenCollection[screenCode].NightStartTime = screen.NightStartTime;
                    _screenCollection[screenCode].IsActive = screen.IsActive;
                }
            }
        }

        // TODO: Test, remove later
        private void ModelTest()
        {
            _screenCollection.Add(new ScreenContextDTO(123, "TestScreeen1", "TestScreeen1"));
            _screenCollection.Add(new ScreenContextDTO(12345, "TestScreeen2", "TestScreeen2")
            {
                NightColorConfiguration = new ColorConfiguration(123, 123)
            });
            _screenCollection.Add(new ScreenContextDTO(123456, "TestScreeen3", "TestScreeen3")
            {
                NightColorConfiguration = new ColorConfiguration(123, 123)
            });
        }
    }
}
