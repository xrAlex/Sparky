using System;
using Common.Entities;
using Common.Enums;
using Common.Extensions.CollectionChanged;
using Common.WinApi;
using Model.Entities;
using Model.Entities.Domain;
using WindowsDisplayAPI;

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
                    _appSettings.ScreenRepository.TryUpdate(args.Screen.DisplayCode, args.PropertyName, args.NewValue);
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
            foreach (var display in Display.GetDisplays())
            {
                var screenContext = TryFormScreenContext(display);
                if (screenContext != null)
                {
                    _screenCollection.Add(screenContext);
                }
            }
        }

        /// <summary>
        /// Создает контекст устройства отображения.
        /// </summary>
        /// <returns><see cref="ScreenContext"/>, в случае успешного получения данных монитора.</returns>
        private ScreenContext? TryFormScreenContext(Display display)
        {
            try
            {
                var screenCode = (int)display.ToPathDisplayTarget().TargetId;
                var screenSystemSettings = TryGetSystemScreenSettings(display);
                var screenUserSettings = TryGetUserSettings(screenCode);
                if (screenSystemSettings != null)
                {
                    return screenUserSettings != null 
                        ? new ScreenContext(screenSystemSettings.Value, screenUserSettings) 
                        : new ScreenContext(screenSystemSettings.Value);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Проверяет наличие пользовательских настроек для уcтройства отображения.
        /// </summary>
        /// <param name="screen"> сформированный объект контекста монитора.</param>
        /// <param name="displayCode"></param>
        /// <returns>При обнаружении настроек устройства отображения возвращает
        /// <see cref="ScreenUserSettings"/></returns>
        private ScreenUserSettings? TryGetUserSettings(int displayCode)
        {
            if (!_appSettings.ScreenRepository.TryGetValue(displayCode, out var screenSettings))
            {
                return null;
            }

            return screenSettings;
        }

        /// <summary>
        /// Получает системные параметры устройства отображения
        /// </summary>
        /// <param name="display">Системное устройство отображения</param>
        /// <returns><see cref="ScreenUserSettings"/> систменые параметры устройства отображения </returns>
        private static ScreenSystemParams? TryGetSystemScreenSettings(Display display)
        {
            if (!display.IsAvailable)
            {
                return null;
            }

            var displayResolution = display.CurrentSetting.Resolution;
            var displayTarget = display.ToPathDisplayTarget();

            var sysParams = new ScreenSystemParams
            (
                (int)displayTarget.TargetId,
                displayTarget.FriendlyName,
                display.DisplayName,
                new ScreenBounds(displayResolution.Width, displayResolution.Height),
                WinApiWrapper.CreateScreenDeviceContext(display.DisplayName)
            );

            return sysParams;
        }
    }
}
