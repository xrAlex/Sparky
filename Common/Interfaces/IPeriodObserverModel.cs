namespace Common.Interfaces
{
    public interface IPeriodObserverModel
    {
        /// <summary>
        /// Запускает цикл обновления настроек гаммы для всех устройств отображения
        /// </summary>
        void StartWatch();

        /// <summary>
        /// Принудительно обновляет гамму на устройства отображения в соответсвии с их настрйоками
        /// </summary>
        public void RefreshAllScreensColorConfiguration();

        /// <summary>
        /// Останавливает цикл обновления гаммы для устройств отображения
        /// </summary>
        void StopWatch();
    }
}
