using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Localization.LangDictionaries;

internal sealed class RusDict : LocalizationDictionary
{
    public LocalizationDictionary Localization { get; }

    public RusDict()
    {
        Localization = new LocalizationDictionary()
        {
            {"LocApply", "Применить"},
            {"LocAutoLaunch", "Запускаться вместе с Windows"},
            {"LocBrightness", "Яркость"},
            {"LocCancel", "Отменить"},
            {"LocDay", "День"},
            {"LocColorTemperature", "Цветовая температура"},
            {"LocDontWorkInFullScreen", "Не работать в полноэкранных приложениях"},
            {"LocExtendedGammaRange", "Расширенный диапозон гаммы"},
            {"LocNight", "Ночь"},
            {"LocApplicationsWhiteList", "Список исключений"},
            {"LocReset", "Сбросить"},
            {"LocSmoothBrightnessChange", "Плавное изменение гаммы"},
            {"LocRestart", "Для применения параметров требуется рестарт"},
            {"LocSunrise", "Восход"},
            {"LocMonitors", "Мониторы"},
            {"LocSunset", "Закат"},
            {"LocToTrayNotification", "Приложение продолжит работу в свернутом состоянии"},
            {"LocTrayClose", "Закрыть"},
            {"LocTrayPause", "Пауза"},
            {"LocTrayUnPause", "Продолжить"}
        };
    }
}