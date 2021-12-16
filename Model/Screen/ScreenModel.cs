using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Extensions.CollectionChanged;
using Common.Interfaces;
using Model.Entities;
using Model.Screen.ScreenCollection;
using Model.Settings;

namespace Model.Screen
{
    public sealed partial class ScreenModel
    {
        private readonly ScreenCollection.ScreenCollection _screenCollection = new();

        public event EventHandler<ScreensCollectionChangedArgs>? ScreensCollectionChanged;

        // TODO: Test, remove later
        private readonly AppSettingsModel _settings = new();
        // TODO: Test, remove later

        public ScreenModel()
        {
            LoadSettings();
            SaveSettings();
            _screenCollection.CollectionChanged += CollectionChanged;
        }

        private void CollectionChanged(object? sender, ScreensCollectionChangedArgs e) 
            => ScreensCollectionChanged?.Invoke(this, e);

        public bool DeleteScreen(int key) 
            => _screenCollection.Remove(key);

        public bool AddScreen(ScreenContextDTO screenDTO) 
            => _screenCollection.Add(screenDTO);

        public IScreenContext? GetScreen(int key) 
            => _screenCollection.TryGetValue(key, out var value)? value : null;

        public IEnumerable<IScreenContext> GetAllScreens() 
            => _screenCollection.Values;

        public void SaveSettings()
        {
            // TODO: Test, remove later

            // перебираем коллекцию мониторов модели и копируем нужные данные в модель настроек приложения
            foreach (var screen in _screenCollection.Values)
            {
                if (!_settings.Screens.ContainsKey(screen.DisplayCode))
                {
                    _settings.Screens.Add(screen.DisplayCode, new ScreenSettings
                    {
                        DayColorConfiguration = screen.DayColorConfiguration,
                        NightColorConfiguration = screen.NightColorConfiguration,
                        DayStartTime = screen.DayStartTime,
                        NightStartTime = screen.NightStartTime
                    });
                }
            }

            // сохраняем нстройки которые скопировали
            _settings.Save();
            // TODO: Test, remove later
        }

        public void LoadSettings()
        {
            // TODO: Test, remove later

            // Создаем тестовые мониторы
            _screenCollection.Add(new ScreenContextDTO(123, "TestScreeen1", "TestScreeen1"));
            _screenCollection.Add(new ScreenContextDTO(12345, "TestScreeen2", "TestScreeen2")
            {
                NightColorConfiguration = new ColorConfiguration(123, 123)
            });
            _screenCollection.Add(new ScreenContextDTO(123456, "TestScreeen2", "TestScreeen2")
            {
                NightColorConfiguration = new ColorConfiguration(123, 123)
            });

            // Загружаем настройки мониторов из файла в класс настроек
            _settings.Load();

            // перебираем мониторы в коллекции настроек
            foreach (var screenCode in _settings.Screens.Keys)
            {
                // если такой монитор есть в модели и в настройках, то копируем данные из настроек
                if (_screenCollection.TryGetValue(screenCode, out var screen))
                {
                    _screenCollection[screenCode].NightColorConfiguration = screen.NightColorConfiguration;
                    _screenCollection[screenCode].DayColorConfiguration = screen.DayColorConfiguration;
                    _screenCollection[screenCode].DayStartTime = screen.DayStartTime;
                    _screenCollection[screenCode].NightStartTime = screen.NightStartTime;
                    _screenCollection[screenCode].IsActive = screen.IsActive;
                }
            }

            // TODO: Test, remove later
        }
    }
}
