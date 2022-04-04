using Common.Infrastructure.Commands;

namespace ViewModel.ViewModels.Settings
{
    public partial class SettingsViewModel
    {
        public RelayCommand RefreshApplicationsList { get; }
        public RelayCommand ApplyValues { get; }
        public RelayCommand ResetValues { get; }

        private void RefreshApplicationsListExecute()
            => _applicationModel.RefreshApplications();

        private void ApplyValuesExecute()
        {
            UnsubscribeEvents();
            _settings.SaveAsync();
            _periodObserverModel.RefreshAllScreensColorConfiguration();
            _periodObserverModel.StartWatch();
        }

        private void ResetValuesExecute()
        {
            UnsubscribeEvents();
            _settings.Reset();
            _periodObserverModel.RefreshAllScreensColorConfiguration();
            _periodObserverModel.StartWatch();
        }
    }
}
