namespace Common.Interfaces;

public interface IApplication
{
    /// <summary>
    /// Имя приложения
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Путь до исполняемого файла приложения
    /// </summary>
    string ExecutableFilePath { get; set; }

    /// <summary>
    /// Работает ли приложение в полноэкранном режиме
    /// </summary>
    bool OnFullScreen { get; set; }

    /// <summary>
    /// Игнорируется ли приложение пользователем
    /// </summary>
    bool IsIgnored { get; set; }
}