using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace View.Localization.WithDto
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
            return new LocalizationDto(xml.language,
                                        xml.apply,
                                        xml.autoLaunch,
                                        xml.brightness,
                                        xml.cancel,
                                        xml.day,
                                        xml.colorTemperature,
                                        xml.dontWorkInFullScreen,
                                        xml.extendedGammaRange,
                                        xml.night,
                                        xml.applicationsWhiteList,
                                        xml.reset,
                                        xml.smoothBrightnessChange,
                                        xml.restart,
                                        xml.sunrise,
                                        xml.monitors,
                                        xml.sunset,
                                        xml.toTrayNotification,
                                        xml.trayClose,
                                        xml.trayPause,
                                        xml.trayUnPause);
        }

        public static LocalizationDto? ParseXml(string fileName)
        {
            using var file = File.OpenRead(fileName);
            return ParseXml(XmlReader.Create(file));
        }

        public static LocalizationDto? ParseEmbeddedXml(string embeddedFileName)
        {
            using var resourceStream = Assembly
                .GetCallingAssembly()
                .GetManifestResourceStream(embeddedFileName);

            if (resourceStream == null)
                return null;

            using var reader = new StreamReader(resourceStream);
            return ParseXml(XmlReader.Create(reader));
        }
    }
}
