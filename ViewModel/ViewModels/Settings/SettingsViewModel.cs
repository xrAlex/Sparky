using System;
using Common.Infrastructure.Commands;
using Common.Infrastructure.ViewModelTemplate;
using Common.Interfaces;

namespace ViewModel.ViewModels.Settings
{
    public sealed partial class SettingsViewModel : ViewModelBase
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
            _checkFullScreensApps = _settings.IsFullScreenAppCheckEnabled;
            RefreshApplicationsList = new RelayCommand(RefreshApplicationsListExecute);
            SaveSettings = new RelayCommand(SaveSettingsExecute);
            ResetSettings = new RelayCommand(ResetSettingsExecute);
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _screenModel.ScreensCollectionChanged += ScreensCollectionChanged;
            _applicationModel.ApplicationCollectionChanged += ApplicationCollectionChanged;
        }

        private void UnsubscribeEvents()
        {
            _screenModel.ScreensCollectionChanged -= ScreensCollectionChanged;
            _applicationModel.ApplicationCollectionChanged -= ApplicationCollectionChanged;
        }
    }
}
