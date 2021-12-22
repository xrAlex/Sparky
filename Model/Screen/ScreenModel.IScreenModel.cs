using System.Collections.Generic;
using Common.DTO;
using Common.Extensions.CollectionChanged;
using Common.Interfaces;
using Model.Entities;

namespace Model.Screen
{
    internal sealed partial class ScreenModel
    {
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
            foreach (var screen in _screenCollection.Values)
            {
                if (!_appSettings.Screens.ContainsKey(screen.DisplayCode))
                {
                    _appSettings.Screens.Add(screen.DisplayCode, new ScreenSettings
                    {
                        DayColorConfiguration = screen.DayColorConfiguration,
                        NightColorConfiguration = screen.NightColorConfiguration,
                        DayStartTime = screen.DayStartTime,
                        NightStartTime = screen.NightStartTime
                    });
                }
            }
        }
    }
}
