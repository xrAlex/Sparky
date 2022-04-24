using System.ComponentModel;
using Common.Entities;
using Common.Enums;

namespace Common.Interfaces;

public interface IScreenContext : INotifyPropertyChanged
{
    /// <summary>
    /// Устанавливает стандартную цветовую конфигурацию для источника отображения
    /// </summary>
    public void SetDefaultColorConfiguration();

    /// <summary>
    /// Текущий период источника отображения
    /// </summary>
    CurrentPeriod CurrentPeriod { get; }

    /// <summary>
    /// Границы (в пикселях) источника отображения
    /// </summary>
    ScreenBounds Bounds { get; }

    /// <summary>
    /// Текущая цветовая конфигурация
    /// </summary>
    ColorConfiguration CurrentColorConfiguration { get; set; }

    /// <summary>
    ///  Конфигурация цветов дневного периода
    /// </summary>
    ColorConfiguration DayColorConfiguration { get; set; }

    /// <summary>
    /// Время начала дневного периода
    /// </summary>
    PeriodStartTime DayStartTime { get; set; }

    /// <summary>
    ///  Уникальный идентификатор устройства отображения
    /// </summary>
    int DisplayCode { get; }

    /// <summary>
    /// Пользовтельское название девайса отображения
    /// </summary>
    string FriendlyName { get; }

    /// <summary>
    /// Дескриптор контекста устройства отображения (WinApi)
    /// </summary>
    nint SystemHandle { get; }

    /// <summary>
    ///  Конфигурация цветов ночного периода
    /// </summary>
    ColorConfiguration NightColorConfiguration { get; set; }

    /// <summary>
    ///  Время начала ночного периода
    /// </summary>
    PeriodStartTime NightStartTime { get; set; }

    /// <summary>
    /// Системное имя источника отображения
    /// </summary>
    string SystemName { get; }
}
