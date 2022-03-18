using Common.Infrastructure.Commands;

namespace ViewModel.ViewModels.Settings
{
    public partial class SettingsViewModel
    {
        public RelayCommand RefreshApplicationsList { get; }
        public RelayCommand SaveSettings { get; }
        public RelayCommand ResetSettings { get; }

        private void RefreshApplicationsListExecute() 
            => _applicationModel.RefreshApplications();

        private void SaveSettingsExecute()
            => _settings.SaveAsync();

        private void ResetSettingsExecute()
            => _settings.Reset();

    }
}
