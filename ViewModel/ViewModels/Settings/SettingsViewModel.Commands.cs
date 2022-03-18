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
        {
            foreach (var appVm in Applications)
            {
                var exePath = appVm.App.ExecutableFilePath;
                if (exePath != null)
                {
                    if (appVm.App.IsIgnored)
                    {
                        if (!_settings.IgnoredApplications.Contains(exePath))
                        {
                            _settings.IgnoredApplications.Add(exePath);
                        }
                    }
                    else
                    {
                        if (_settings.IgnoredApplications.Contains(exePath))
                        {
                            _settings.IgnoredApplications.Remove(exePath);
                        }
                    }
                }
            }
            _settings.SaveAsync();
        }


        private void ResetSettingsExecute()
            => _settings.Reset();
    }
}
