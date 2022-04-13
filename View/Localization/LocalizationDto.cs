using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace View.Localization
{
    public class LocalizationDto
    {
        public string? Language { get; }
        public string? Apply { get; }
        public string? AutoLaunch { get; }
        public string? Brightness { get; }
        public string? Cancel { get; }
        public string? Day { get; }
        public string? ColorTemperature { get; }
        public string? DontWorkInFullScreen { get; }
        public string? ExtendedGammaRange { get; }
        public string? Night { get; }
        public string? ApplicationsWhiteList { get; }
        public string? Reset { get; }
        public string? SmoothBrightnessChange { get; }
        public string? Restart { get; }
        public string? Sunrise { get; }
        public string? Monitors { get; }
        public string? Sunset { get; }
        public string? ToTrayNotification { get; }
        public string? TrayClose { get; }
        public string? TrayPause { get; }
        public string? TrayUnPause { get; }

        public LocalizationDto(string? language,
                               string? apply,
                               string? autoLaunch,
                               string? brightness,
                               string? cancel,
                               string? day,
                               string? colorTemperature,
                               string? dontWorkInFullScreen,
                               string? extendedGammaRange,
                               string? night,
                               string? applicationsWhiteList,
                               string? reset,
                               string? smoothBrightnessChange,
                               string? restart,
                               string? sunrise,
                               string? monitors,
                               string? sunset,
                               string? toTrayNotification,
                               string? trayClose,
                               string? trayPause,
                               string? trayUnPause)
        {
            Language = language;
            Apply = apply;
            AutoLaunch = autoLaunch;
            Brightness = brightness;
            Cancel = cancel;
            Day = day;
            ColorTemperature = colorTemperature;
            DontWorkInFullScreen = dontWorkInFullScreen;
            ExtendedGammaRange = extendedGammaRange;
            Night = night;
            ApplicationsWhiteList = applicationsWhiteList;
            Reset = reset;
            SmoothBrightnessChange = smoothBrightnessChange;
            Restart = restart;
            Sunrise = sunrise;
            Monitors = monitors;
            Sunset = sunset;
            ToTrayNotification = toTrayNotification;
            TrayClose = trayClose;
            TrayPause = trayPause;
            TrayUnPause = trayUnPause;
        }

        private static readonly XmlSerializer LocalizationSerializer = new(typeof(LocalizationXml));

        public static LocalizationDto? ParseXml(XmlReader reader)
        {
            var xml = (LocalizationXml?)LocalizationSerializer.Deserialize(reader);
            if (xml == null)
                return null;
            return new LocalizationDto(xml.Language,
                                        xml.Apply,
                                        xml.AutoLaunch,
                                        xml.Brightness,
                                        xml.Cancel,
                                        xml.Day,
                                        xml.ColorTemperature,
                                        xml.DontWorkInFullScreen,
                                        xml.ExtendedGammaRange,
                                        xml.Night,
                                        xml.ApplicationsWhiteList,
                                        xml.Reset,
                                        xml.SmoothBrightnessChange,
                                        xml.Restart,
                                        xml.Sunrise,
                                        xml.Monitors,
                                        xml.Sunset,
                                        xml.ToTrayNotification,
                                        xml.TrayClose,
                                        xml.TrayPause,
                                        xml.TrayUnPause);
        }

        public static LocalizationDto? ParseXml(string fileName)
        {
            using var file = File.OpenRead(fileName);
            return ParseXml(XmlReader.Create(file));
        }
    }
}
