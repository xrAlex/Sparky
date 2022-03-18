using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IAppSettingsModel
    {
        /// <summary>
        /// Список имен приложений которые игнорируются в полноэкранном режиме
        /// </summary>
         List<string> IgnoredApplications { get; }

        /// <summary>
        /// Загружает настройки приложения из файла
        /// </summary>
        void Load();

        /// <summary>
        /// Асинхронно загружает настройки приложения из файла
        /// </summary>
        Task LoadAsync();

        /// <summary>
        /// Сохраняет настройки настройки приложения в файл
        /// </summary>
        void Save();

        /// <summary>
        /// Очищает данные и заново подгружает настройки приложения из файла
        /// </summary>
        void Reset();

        /// <summary>
        /// Асинхронно очищает данные и заново подгружает настройки приложения из файла
        /// </summary>
        Task ResetAsync();

        /// <summary>
        /// Асинхронно сохраняет настройки настройки приложения в файл
        /// </summary>
        Task SaveAsync();

        /// <summary>
        /// Событие сброса настроек
        /// </summary>
        event EventHandler? SettingsReset;

        /// <summary>
        /// Событие сохранения настроек
        /// </summary>
        event EventHandler? SettingsSaved;

        /// <summary>
        /// Событие загрузки настроек
        /// </summary>
        event EventHandler? SettingsLoaded;
    }
}