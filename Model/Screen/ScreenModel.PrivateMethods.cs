using System;
using Common.Entities;
using Common.Enums;
using Common.Extensions.CollectionChanged;
using Model.Entities;
using WindowsDisplayAPI.DisplayConfig;

namespace Model.Screen
{
    internal sealed partial class ScreenModel
    {
        /// <summary>
        /// Событие изменения внутренней коллекции модели.
        /// </summary>
        private void ScreenCollectionChanged(object? sender, ScreensCollectionChangedArgs args)
        {
            switch (args.Action)
            {
                case CollectionChangedAction.Added:
                    _appSettings.ScreenRepository.TryAdd(args.Screen.DisplayCode, (ScreenContext)args.Screen);
                    break;
                case CollectionChangedAction.Removed:
                    _appSettings.ScreenRepository.Delete(args.Screen.DisplayCode);
                    break;
                case CollectionChangedAction.Updated:
                    _appSettings.ScreenRepository.TryUpdate(args.Screen.DisplayCode, (ScreenContext)args.Screen);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(args.ToString());
            }
            InternalCollectionChanged?.Invoke(this, args);
        }

        /// <summary>
        /// Формирует коллекцию устройств отображения.
        /// </summary>
        private void LoadScreens()
        {
            foreach (var display in PathDisplayTarget.GetDisplayTargets())
            {
                var screen = TryFormScreenContext(display);
                if (screen == null) continue;

                var screenContext = TryGetUserSettings(screen);
                _screenCollection.Add(screenContext);
            }
        }

        /// <summary>
        /// Создает контекст устройства отображения.
        /// </summary>
        /// <returns><see cref="ScreenContextDTO"/>, в случае успешного получения данных монитора.</returns>
        private static ScreenContext? TryFormScreenContext(PathDisplayTarget display)
        {
            try
            {
                var device = display.ToDisplayDevice();
                var settings = device.GetPreferredSetting();
                var screen = new ScreenContext(display.EDIDProductCode, device.DisplayName, display.FriendlyName)
                {
                    IsActive = true,
                    CurrentColorConfiguration = new ColorConfiguration(6600f, 1f),
                    DayColorConfiguration = new ColorConfiguration(6600f, 1f),
                    NightColorConfiguration = new ColorConfiguration(5200f, 0.7f),
                    DayStartTime = new PeriodStartTime(7, 0),
                    NightStartTime = new PeriodStartTime(22, 0),
                    Bounds = new ScreenBounds(settings.Resolution.Width, settings.Resolution.Height)
                };

                return screen;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Проверяет наличие пользовательских настроек для уcтройства отображения.
        /// </summary>
        /// <param name="screen"> сформированный DTO контекста монитора.</param>
        /// <returns>В случае обнаружения настроек устройства отображения возвращает <see cref="ScreenContext"/>
        /// с этими настройками или возвращает неизменный экземпляр.</returns>
        private ScreenContext TryGetUserSettings(ScreenContext screen)
        {
            if (!_appSettings.ScreenRepository.TryGetValue(screen.DisplayCode, out var screenSettings))
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
