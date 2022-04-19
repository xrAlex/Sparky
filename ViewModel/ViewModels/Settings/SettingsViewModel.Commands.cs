using Common.Infrastructure.Commands;

namespace ViewModel.ViewModels.Settings;

public partial class SettingsViewModel
{
    public RelayCommand RefreshApplicationsList { get; }
    public RelayCommand ApplyValues { get; }
    public RelayCommand ResetValues { get; }
    public RelayCommand ResetSettings { get; }

    private void RefreshApplicationsListExecute()
        => _applicationModel.RefreshApplications();

    private void ApplyValuesExecute()
    {
        UnsubscribeEvents();
        _settingsModel.SaveAsync();
        _periodObserverModel.RefreshAllScreensColorConfiguration();
        _periodObserverModel.StartWatch();
    }

    private void ResetValuesExecute()
    {
        UnsubscribeEvents();
        _settingsModel.Reset();
        _periodObserverModel.RefreshAllScreensColorConfiguration();
        _periodObserverModel.StartWatch();
    }

    private void ResetSettingsExecute() 
        => _settingsModel.Reset();
}