using System.ComponentModel;

namespace Common.Interfaces;

public interface IApplication : INotifyPropertyChanged
{
    /// <summary>
    /// Имя приложения
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Путь до исполняемого файла приложения
    /// </summary>
    string ExecutableFilePath { get; }

    /// <summary>
    /// Работает ли приложение в полноэкранном режиме
    /// </summary>
    bool OnFullScreen { get; }

    /// <summary>
    /// Игнорируется ли приложение пользователем
    /// </summary>
    bool IsIgnored { get; set; }
}