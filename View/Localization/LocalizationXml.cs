using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace View.Localization
{
    // Примечание. Для запуска созданного кода может потребоваться NET Framework версии 4.5 или более поздней версии и .NET Core или Standard версии 2.0 или более поздней.
    /// <remarks/>
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName="Localization")]
    public class LocalizationXml
    {
        [XmlAttribute()]
        public string? Language;

        [XmlAttribute()]
        public string? Apply;

        [XmlAttribute()]
        public string? AutoLaunch;

        [XmlAttribute()]
        public string? Brightness;

        [XmlAttribute()]
        public string? Cancel;

        [XmlAttribute()]
        public string? Day;

        [XmlAttribute()]
        public string? ColorTemperature;

        [XmlAttribute()]
        public string? DontWorkInFullScreen;

        [XmlAttribute()]
        public string? ExtendedGammaRange;

        [XmlAttribute()]
        public string? Night;

        [XmlAttribute()]
        public string? ApplicationsWhiteList;

        [XmlAttribute()]
        public string? Reset;

        [XmlAttribute()]
        public string? SmoothBrightnessChange;

        [XmlAttribute()]
        public string? Restart;

        [XmlAttribute()]
        public string? Sunrise;

        [XmlAttribute()]
        public string? Monitors;

        [XmlAttribute()]
        public string? Sunset;

        [XmlAttribute()]
        public string? ToTrayNotification;

        [XmlAttribute()]
        public string? TrayClose;

        [XmlAttribute()]
        public string? TrayPause;

        [XmlAttribute()]
        public string? TrayUnPause;
    }
}
