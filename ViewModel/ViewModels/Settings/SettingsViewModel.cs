using Common.Infrastructure.Commands;
using Common.Infrastructure.ViewModelTemplate;
using Common.Interfaces;

namespace ViewModel.ViewModels.Settings
{
    public partial class SettingsViewModel : ViewModelBase
    {
        private readonly IScreenModel _screenModel;
        private readonly IApplicationModel _applicationModel;
        private readonly IAppSettingsModel _settings;

        public SettingsViewModel(IScreenModel screenModel,
            IApplicationModel applicationModel,
            IAppSettingsModel settings)
        {
            _screenModel = screenModel;
            _applicationModel = applicationModel;
            _settings = settings;
            RefreshApplicationsList = new RelayCommand(RefreshApplicationsListExecute);
            SaveSettings = new RelayCommand(SaveSettingsExecute);
            ResetSettings = new RelayCommand(ResetSettingsExecute);

            // TODO: отписываемся при Dispose
            screenModel.ScreensCollectionChanged += ScreensCollectionChanged;
            applicationModel.ApplicationCollectionChanged += ApplicationCollectionChanged;
        }
    }
}
