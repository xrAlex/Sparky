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
