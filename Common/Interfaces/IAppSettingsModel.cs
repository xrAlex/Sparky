using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IAppSettingsModel
    {
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
        /// Асинхронно сохраняет настройки настройки приложения в файл
        /// </summary>
        Task SaveAsync();
    }
}