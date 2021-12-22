using System;
using System.Collections.Generic;
using Common.DTO;
using Common.Extensions.CollectionChanged;

namespace Common.Interfaces
{
    public interface IScreenModel
    {
        /// <summary>
        /// Событие оповещающее об изменении внутренней коллекции мониторов.
        /// </summary>
        /// <remarks>При подписке на событие возвращает все элементы коллекции.</remarks>
        event EventHandler<ScreensCollectionChangedArgs>? ScreensCollectionChanged;

        /// <summary>
        /// Создание и добавление модели устройства отображения в коллекцию устройств отображения.
        /// </summary>
        /// <param name="screenDTO">Объект передачи данных устройства отображения.</param>
        /// <returns><see langword="true"/>, если добавление произошло успешно.</returns>
        bool AddScreen(ScreenContextDTO screenDTO);

        /// <summary>
        /// Удаление источника отображения.
        /// </summary>
        /// <param name="key">DisplayCode источника отображения.</param>
        /// <returns><see langword="true"/>, если удаление произошло успешно.</returns>
        bool DeleteScreen(int key);

        /// <summary>
        /// Возвращает интерфейсы всех устройств отображения.
        /// </summary>
        IEnumerable<IScreenContext> GetAllScreens();

        /// <summary>
        /// Возвращает устройство отображения по ключу.
        /// </summary>
        /// <param name="key">DisplayCode источника отображения.</param>
        IScreenContext? GetScreen(int key);

        /// <summary>
        /// Сохраняет данные моделей в класс пользовательских настроек.
        /// </summary>
        void SaveSettings();
    }
}