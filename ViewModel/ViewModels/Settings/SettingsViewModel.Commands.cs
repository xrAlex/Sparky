using Common.Infrastructure.Commands;

namespace ViewModel.ViewModels.Settings
{
    public partial class SettingsViewModel
    {
        public RelayCommand RefreshApplicationsList { get; }

        private void RefreshApplicationsListExecute() 
            => _applicationModel.RefreshApplications();

    }
}
