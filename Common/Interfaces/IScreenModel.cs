using System;
using System.Collections.Generic;
using Common.Extensions.CollectionChanged;

namespace Common.Interfaces;

public interface IScreenModel
{
    /// <summary>
    /// Событие оповещающее об изменении внутренней коллекции мониторов.
    /// </summary>
    /// <remarks>При подписке на событие возвращает все элементы коллекции.</remarks>
    event EventHandler<ScreensCollectionChangedArgs>? ScreensCollectionChanged;

    /// <summary>
    /// Возвращает интерфейсы всех устройств отображения.
    /// </summary>
    IEnumerable<IScreenContext> GetAllScreens();

    /// <summary>
    /// Возвращает устройство отображения по ключу.
    /// </summary>
    /// <param name="key">DisplayCode источника отображения.</param>
    IScreenContext? GetScreen(int key);
}