using System;
using Common.DTO;
using Common.Entities;
using Common.Extensions.CollectionChanged;
using WindowsDisplayAPI.DisplayConfig;

namespace Model.Screen
{
    internal sealed partial class ScreenModel
    {

        private void CollectionChanged(object? sender, ScreensCollectionChangedArgs e)
            => InternalCollectionChanged?.Invoke(this, e);

        /// <summary>
        /// Формирует коллекцию устройств отображения.
        /// </summary>
        private void LoadScreens()
        {
            foreach (var display in PathDisplayTarget.GetDisplayTargets())
            {
                var screen = TryFormScreenContext(display);
                if (screen == null) continue;

                var screenContext = TryGetUserSettings(screen.Value);
                _screenCollection.Add(screenContext);
            }
        }

        /// <summary>
        /// Создает контекст устройства отображения.
        /// </summary>
        /// <returns><see cref="ScreenContextDTO"/>, в случае успешного получения данных монитора.</returns>
        private ScreenContextDTO? TryFormScreenContext(PathDisplayTarget display)
        {
            try
            {
                var device = display.ToDisplayDevice();
                var settings = device.GetPreferredSetting();

                return new ScreenContextDTO(
                    display.EDIDProductCode,
                    device.DisplayName,
                    display.FriendlyName,
                    new ScreenBounds(settings.Resolution.Width, settings.Resolution.Height));
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Проверяет наличие пользовательских настроек для утройства отображения.
        /// </summary>
        /// <param name="screen"> сформированный DTO контекста монитора.</param>
        /// <returns>В случае обнаружения настроек устройства отображения возвращает <see cref="ScreenContextDTO"/>
        /// с этими настройками или возвращает неизменней экземпляр.</returns>
        private ScreenContextDTO TryGetUserSettings(ScreenContextDTO screen)
        {
            if (!_appSettings.Screens.TryGetValue(screen.DisplayCode, out var screenSettings))
            {
                return screen;
            }

            screen.NightColorConfiguration = screenSettings.NightColorConfiguration;
            screen.DayColorConfiguration = screenSettings.DayColorConfiguration;
            screen.DayStartTime = screenSettings.DayStartTime;
            screen.NightStartTime = screenSettings.NightStartTime;
            screen.IsActive = screenSettings.IsActive;

            return screen;
        }
    }
}
