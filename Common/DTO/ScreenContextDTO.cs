using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;
using Common.Interfaces;

namespace Common.DTO
{
    /// <summary>
    /// Screen Data Transfer Object
    /// </summary>
    public struct ScreenContextDTO : IScreenContext
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

        public ScreenContextDTO(int displayCode, string systemName, string friendlyName) : this()
        {
            if (string.IsNullOrWhiteSpace(systemName))
            {
                throw new ArgumentNullException(nameof(systemName));
            }

            DisplayCode = displayCode;
            SystemName = systemName;
            FriendlyName = friendlyName;
        }
    }
}
