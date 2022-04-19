using Common.Entities;
using Newtonsoft.Json;

namespace Model.Entities;

/// <summary>
/// Пользовательские настройки устрйоства отображения
/// </summary>
internal sealed class ScreenUserSettings
{
    [JsonProperty("Night color configuration")]
    public ColorConfiguration NightColorConfiguration { get; set; }

    [JsonProperty("Day color configuration")]
    public ColorConfiguration DayColorConfiguration { get; set; }

    [JsonProperty("Night period start time")]
    public PeriodStartTime NightStartTime { get; set; }

    [JsonProperty("Day period start time")]
    public PeriodStartTime DayStartTime { get; set; }

    [JsonProperty("Screen active")]
    public bool IsActive { get; set; }

    [JsonProperty("Screen display code")]
    public int DisplayCode { get; }

    public ScreenUserSettings(int displayCode)
    {
        DisplayCode = displayCode;
    }
}