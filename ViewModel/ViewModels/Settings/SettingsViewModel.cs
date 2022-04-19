using Common.Infrastructure.Commands;
using Common.Infrastructure.ViewModelTemplate;
using Common.Interfaces;

namespace ViewModel.ViewModels.Settings;

public sealed partial class SettingsViewModel : ViewModelBase
{
    private readonly IScreenModel _screenModel;
    private readonly IApplicationModel _applicationModel;
    private readonly IAppSettingsModel _settingsModel;
    private readonly IRegistryModel _registryModel;
    private readonly IPeriodObserverModel _periodObserverModel;

    public SettingsViewModel(IScreenModel screenModel,
        IApplicationModel applicationModel,
        IAppSettingsModel settings, IRegistryModel registryModel,
        IPeriodObserverModel periodObserver)
    {
        _screenModel = screenModel;
        _applicationModel = applicationModel;
        _settingsModel = settings;
        _registryModel = registryModel;
        _periodObserverModel = periodObserver;
        _checkFullScreensApps = _settingsModel.IsFullScreenAppCheckEnabled;
        _autoLaunchOnStartup = registryModel.IsAppStartupKeyFounded();
        _extendedGammaRangeEnabled = registryModel.IsExtendedGammaRangeActive();
        _gammaSmoothingEnabled = settings.IsGammaSmoothingEnabled;
 
        RefreshApplicationsList = new RelayCommand(RefreshApplicationsListExecute);
        ApplyValues = new RelayCommand(ApplyValuesExecute);
        ResetValues = new RelayCommand(ResetValuesExecute);
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