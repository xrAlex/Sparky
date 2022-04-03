using Common.Entities;
using WindowsDisplayAPI.Native.Structures;

namespace Model.Entities
{
    internal class ScreenUserSettings
    {
        public ColorConfiguration NightColorConfiguration { get; set; }
        public ColorConfiguration DayColorConfiguration { get; set; }
        public PeriodStartTime NightStartTime { get; set; }
        public PeriodStartTime DayStartTime { get; set; }
        public ScreenBounds Bounds { get; set; }
        public bool IsActive { get; set; } = true;
        public int DisplayCode { get; }

        public ScreenUserSettings(int displayCode)
        {
            DisplayCode = displayCode;
        }
    }
}
