namespace Common.Interfaces;

public interface IRegistryModel
{
    /// <summary>
    /// Проверяет наличие в реестре ключа автозапуска приложения
    /// </summary>
    bool IsAppStartupKeyFounded();

    /// <summary>
    /// Проверяет наличие в реестре ключа расширенного диапозона гаммы
    /// </summary>
    bool IsExtendedGammaRangeActive();

    /// <summary>
    /// Добавляет в реестр ключ автозапуска приложения
    /// </summary>
    void AddAppStartupKey();

    /// <summary>
    /// Удаляет из реестра ключ автозапуска приложения
    /// </summary>
    void DeleteAppStartupKey();

    /// <summary>
    /// Устанавливает стандартное значение диапозона гаммы в реестре
    /// </summary>
    void SetDefaultGammaRangeKey();

    /// <summary>
    /// Устанавливает значение расширяющее диапозон гаммы системы в реестре
    /// </summary>
    void SetExtendedGammaRangeKey();
}