using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IPeriodObserverModel
    {
        /// <summary>
        /// Starts current period watcher cycle
        /// </summary>
        void StartWatch();

        public void RefreshAllScreensColorConfiguration();

        /// <summary>
        /// Stops current period watcher cycle
        /// </summary>
        void StopWatch();
    }
}
