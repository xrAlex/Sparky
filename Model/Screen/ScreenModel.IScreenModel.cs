using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;
using Common.Extensions.CollectionChanged;
using Common.Interfaces;

namespace Model.Screen
{
    public sealed partial class ScreenModel
    {
        private void CollectionChanged(object? sender, ScreensCollectionChangedArgs e)
            => ScreensCollectionChanged?.Invoke(this, e);

        /// <inheritdoc cref="IScreenModel.DeleteScreen"/>
        public bool DeleteScreen(int key)
            => _screenCollection.Remove(key);

        /// <inheritdoc cref="IScreenModel.AddScreen"/>
        public bool AddScreen(ScreenContextDTO screenDTO)
            => _screenCollection.Add(screenDTO);

        /// <inheritdoc cref="IScreenModel.GetScreen"/>
        public IScreenContext? GetScreen(int key)
            => _screenCollection.TryGetValue(key, out var value) ? value : null;

        /// <inheritdoc cref="IScreenModel.GetAllScreens"/>
        public IEnumerable<IScreenContext> GetAllScreens()
            => _screenCollection.Values;

        /// <inheritdoc cref="IScreenModel.SaveSettings"/>
        public void SaveSettings()
        {
            //foreach (var screen in _screenCollection.Values)
            //{
            //    if (!_settings.Screens.ContainsKey(screen.DisplayCode))
            //    {
            //        _settings.Screens.Add(screen.DisplayCode, new ScreenSettings
            //        {
            //            DayColorConfiguration = screen.DayColorConfiguration,
            //            NightColorConfiguration = screen.NightColorConfiguration,
            //            DayStartTime = screen.DayStartTime,
            //            NightStartTime = screen.NightStartTime
            //        });
            //    }
            //}
        }

        /// <inheritdoc cref="IScreenModel.LoadSettings"/>
        public void LoadSettings()
        {
            //foreach (var screenCode in _settings.Screens.Keys)
            //{
            //    if (_screenCollection.TryGetValue(screenCode, out var screen))
            //    {
            //        _screenCollection[screenCode].NightColorConfiguration = screen.NightColorConfiguration;
            //        _screenCollection[screenCode].DayColorConfiguration = screen.DayColorConfiguration;
            //        _screenCollection[screenCode].DayStartTime = screen.DayStartTime;
            //        _screenCollection[screenCode].NightStartTime = screen.NightStartTime;
            //        _screenCollection[screenCode].IsActive = screen.IsActive;
            //    }
            //}
        }
    }
}
