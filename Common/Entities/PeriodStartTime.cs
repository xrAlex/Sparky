using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public readonly struct PeriodStartTime
    {
        public int Hour { get; }
        public int Minute { get; }

        public PeriodStartTime(int hour, int minute)
        {
            Hour = hour;
            Minute = minute;
        }
    }
}
