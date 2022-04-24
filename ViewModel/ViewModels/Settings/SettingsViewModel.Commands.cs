using Common.Infrastructure.Commands;

namespace ViewModel.ViewModels.Settings;

public partial class SettingsViewModel
{
    public RelayCommand RefreshApplicationsList { get; }
    public RelayCommand ApplyValues { get; }
    public RelayCommand ResetValues { get; }
    public RelayCommand ResetSettings { get; }

    /// <summary>
    /// Команда обновления коллекции приложений
    /// </summary>
    private void RefreshApplicationsListExecute()
        => _applicationModel.RefreshApplications();

    /// <summary>
    /// Команда сохранения настроек приложения
    /// </summary>
    private void ApplyValuesExecute()
    {
        UnsubscribeEvents();
        _settingsModel.SaveAsync();
        _periodObserverModel.RefreshAllScreensColorConfiguration();
        _periodObserverModel.StartWatch();
    }

    /// <summary>
    /// Команда сброса настроек и перезапуска цикла PeriodObserverModel
    /// </summary>
    private void ResetValuesExecute()
    {
        UnsubscribeEvents();
        _settingsModel.Reset();
        _periodObserverModel.RefreshAllScreensColorConfiguration();
        _periodObserverModel.StartWatch();
    }

    /// <summary>
    /// Команда сброса настроек приложения
    /// </summary>
    private void ResetSettingsExecute() 
        => _settingsModel.Reset();
}