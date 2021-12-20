using Common.Entities;

namespace Model.Entities
{
    internal struct ScreenSettings
    {
        public ColorConfiguration NightColorConfiguration { get; set; }
        public ColorConfiguration DayColorConfiguration { get; set; }
        public PeriodStartTime NightStartTime { get; set; }
        public PeriodStartTime DayStartTime { get; set; }
        public bool IsActive { get; set; }
    }
}
