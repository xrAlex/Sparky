using System;
using Common.Entities;
using Common.Interfaces;

namespace Common.DTO
{
    /// <summary>
    /// Data Transfer Object устройства отображения
    /// </summary>
    public class ScreenContextDTO : IScreenContext
    {
        public string SystemName { get; }

        public string FriendlyName { get; }

        public int DisplayCode { get; }

        public bool IsActive { get; set; }

        public ColorConfiguration DayColorConfiguration { get; set; }

        public ColorConfiguration NightColorConfiguration { get; set; }

        public ColorConfiguration CurrentColorConfiguration { get; set; }

        public PeriodStartTime DayStartTime { get; set; }

        public PeriodStartTime NightStartTime { get; set; }

        public ScreenBounds Bounds { get; set; }

        public ScreenContextDTO(int displayCode, string systemName, string friendlyName)
        {
            if (string.IsNullOrWhiteSpace(systemName))
            {
                throw new ArgumentNullException(nameof(systemName));
            }

            DisplayCode = displayCode;
            SystemName = systemName;
            FriendlyName = friendlyName;
        }

        public ScreenContextDTO(int displayCode, string systemName, string friendlyName, ScreenBounds bounds)
        {
            if (string.IsNullOrWhiteSpace(systemName))
            {
                throw new ArgumentNullException(nameof(systemName));
            }

            DisplayCode = displayCode;
            SystemName = systemName;
            FriendlyName = friendlyName;
            Bounds = bounds;
        }
    }
}
