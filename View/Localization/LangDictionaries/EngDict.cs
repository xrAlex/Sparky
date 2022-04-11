using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Localization.LangDictionaries
{
    internal class EngDict
    {
        public LocalizationDictionary Localization { get; }

        public EngDict()
        {
            Localization = new LocalizationDictionary()
            {
                {"LocApply", "Apply"},
                {"LocAutoLaunch", "Start with Windows"},
                {"LocBrightness", "Brightness"},
                {"LocCancel", "Cancel"},
                {"LocDay", "Day"},
                {"LocColorTemperature", "Color temperature"},
                {"LocDontWorkInFullScreen", "Dont work in full screen apllications"},
                {"LocExtendedGammaRange", "Extended gamma range"},
                {"LocNight", "Night"},
                {"LocApplicationsWhiteList", "Applications whitelist"},
                {"LocReset", "Reset"},
                {"LocSmoothBrightnessChange", "Smooth brightness change"},
                {"LocRestart", "Restart required to apply parameters"},
                {"LocSunrise", "Sunrise"},
                {"LocMonitors", "Monitors"},
                {"LocSunset", "Sunset"},
                {"LocToTrayNotification", "Application will continue to work in a collapsed state"},
                {"LocTrayClose", "Close"},
                {"LocTrayPause", "Pause"},
                {"LocTrayUnPause", "Resume"}
            };
        }
    }
}
