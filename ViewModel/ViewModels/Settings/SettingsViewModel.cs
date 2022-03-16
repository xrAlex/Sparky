using Common.Infrastructure.Commands;
using Common.Infrastructure.ViewModelTemplate;
using Common.Interfaces;

namespace ViewModel.ViewModels.Settings
{
    public partial class SettingsViewModel : ViewModelBase
    {
        private readonly IScreenModel _screenModel;
        private readonly IApplicationModel _applicationModel;

        public SettingsViewModel(IScreenModel screenModel, IApplicationModel applicationModel)
        {
            _screenModel = screenModel;
            _applicationModel = applicationModel;
            RefreshApplicationsList = new RelayCommand(RefreshApplicationsListExecute);

            screenModel.ScreensCollectionChanged += ScreensCollectionChanged;
            applicationModel.ApplicationCollectionChanged += ApplicationCollectionChanged;
        }
    }
}
