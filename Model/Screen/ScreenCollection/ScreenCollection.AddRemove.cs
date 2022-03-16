using Common.DTO;
using Common.Enums;
using Common.Extensions.CollectionChanged;
using Model.Entities;

namespace Model.Screen.ScreenCollection
{
    internal sealed partial class ScreenCollection
    {
        /// <summary>
        /// Создание источника отображения на основе DTO и добавление его в коллекцию.
        /// </summary>
        /// <param name="screenDTO">Объект передачи данных устройства отображения.</param>
        /// <returns><see langword="true"/>, если добавление произошло успешно.</returns>
        public bool Add(ScreenContextDTO screenDTO)
        {
            if (ContainsKey(screenDTO.DisplayCode))
            {
                return false;
            }

            var screen = new ScreenContext(screenDTO.DisplayCode, screenDTO.SystemName, screenDTO.FriendlyName)
            {
                IsActive = screenDTO.IsActive,
                Bounds = screenDTO.Bounds,
                CurrentColorConfiguration = screenDTO.CurrentColorConfiguration,
                DayColorConfiguration = screenDTO.DayColorConfiguration,
                DayStartTime = screenDTO.DayStartTime,
                NightColorConfiguration = screenDTO.NightColorConfiguration,
                NightStartTime = screenDTO.NightStartTime
            };

            _screens.Add(screen.DisplayCode, screen);

            CollectionChanged?.Invoke(this, new ScreensCollectionChangedArgs(screen, CollectionChangedAction.Added));
            return true;
        }

        /// <summary>
        /// Удаление источника отображения из коллекции.
        /// </summary>
        /// <param name="key">DisplayCode источника отображения.</param>
        /// <returns><see langword="true"/>, если удаление произошло успешно.</returns>
        public bool Remove(int key)
        {
            if (!TryGetValue(key, out var screen))
            {
                return false;
            }

            _screens.Remove(key);

            CollectionChanged?.Invoke(this, new ScreensCollectionChangedArgs(screen, CollectionChangedAction.Removed));
            return true;
        }
    }
}
