using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

namespace View.Localization.WithDto
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
        public string? language;

        [XmlAttribute()]
        public string? apply;

        [XmlAttribute()]
        public string? autoLaunch;

        [XmlAttribute()]
        public string? brightness;

        [XmlAttribute()]
        public string? cancel;

        [XmlAttribute()]
        public string? day;

        [XmlAttribute()]
        public string? colorTemperature;

        [XmlAttribute()]
        public string? dontWorkInFullScreen;

        [XmlAttribute()]
        public string? extendedGammaRange;

        [XmlAttribute()]
        public string? night;

        [XmlAttribute()]
        public string? applicationsWhiteList;

        [XmlAttribute()]
        public string? reset;

        [XmlAttribute()]
        public string? smoothBrightnessChange;

        [XmlAttribute()]
        public string? restart;

        [XmlAttribute()]
        public string? sunrise;

        [XmlAttribute()]
        public string? monitors;

        [XmlAttribute()]
        public string? sunset;

        [XmlAttribute()]
        public string? toTrayNotification;

        [XmlAttribute()]
        public string? trayClose;

        [XmlAttribute()]
        public string? trayPause;

        [XmlAttribute()]
        public string? trayUnPause;
    }
}
