using Common.Entities;

namespace Model.Entities;

internal sealed class ScreenUserSettings
{
    public ColorConfiguration NightColorConfiguration { get; set; }
    public ColorConfiguration DayColorConfiguration { get; set; }
    public PeriodStartTime NightStartTime { get; set; }
    public PeriodStartTime DayStartTime { get; set; }
    public bool IsActive { get; set; }
    public int DisplayCode { get; }

    public ScreenUserSettings(int displayCode)
    {
        DisplayCode = displayCode;
    }
}