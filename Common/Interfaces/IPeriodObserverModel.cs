using System;

namespace Common.Interfaces;

public interface IPeriodObserverModel
{
    /// <summary>
    /// Запускает цикл обновления настроек гаммы для всех устройств отображения
    /// </summary>
    void StartWatch();

    /// <summary>
    /// Принудительно обновляет гамму на устройства отображения в соответсвии с их настройками
    /// </summary>
    public void RefreshAllScreensColorConfiguration();

    /// <summary>
    /// Форсирует установку гаммы к стандартным значениям
    /// </summary>
    public void ForceDefaultColorConfiguration();

    /// <summary>
    /// Останавливает цикл обновления гаммы для устройств отображения
    /// </summary>
    void StopWatch();

    /// <summary>
    /// Событие оповещающее о начале работы цикла обсервера
    /// </summary>
    public event EventHandler ObserverStarted;

    /// <summary>
    /// Событие оповещающее о остановке работы цикла обсервера
    /// </summary>
    public event EventHandler ObserverStopped;
}