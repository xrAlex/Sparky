using System;
using System.Collections.ObjectModel;
using Common.Enums;
using Common.Extensions;
using Common.Extensions.CollectionChanged;
using ViewModel.SubViewModels;

namespace ViewModel.ViewModels.Settings;

public partial class SettingsViewModel
{
    public ObservableCollection<ScreenViewModel> Screens { get; } = new();
    public ObservableCollection<ApplicationViewModel> Applications { get; } = new();

    private bool _checkFullScreensApps;
    private ScreenViewModel? _selectedScreen;
    private bool _autoLaunchOnStartup;
    private bool _extendedGammaRangeEnabled;
    private bool _gammaSmoothingEnabled;

    public int MinGammaRange => _registryModel.IsExtendedGammaRangeActive() ? 1000 : 4200;
    public int MinBrightnessRange => _registryModel.IsExtendedGammaRangeActive() ? 10 : 70;

    public ScreenViewModel? SelectedScreen
    {
        get => _selectedScreen;
        set => Set(ref _selectedScreen, value);
    }

    public bool CheckFullScreensApps
    {
        get => _checkFullScreensApps;
        set => Set(ref _checkFullScreensApps, value);
    }

    public bool AutoLaunchOnStartup
    {
        get => _autoLaunchOnStartup;
        set => Set(ref _autoLaunchOnStartup, value);
    }

    public bool IsExtendedGammaRangeEnabled
    {
        get => _extendedGammaRangeEnabled;
        set => Set(ref _extendedGammaRangeEnabled, value);
    }

    public bool IsGammaSmoothingEnabled
    {
        get => _gammaSmoothingEnabled;
        set => Set(ref _gammaSmoothingEnabled, value);
    }

    protected override void OnPropertyChanged(in string propertyName, in object oldValue, in object newValue)
    {
        base.OnPropertyChanged(in propertyName, in oldValue, in newValue);

        switch (propertyName)
        {
            case nameof(CheckFullScreensApps):
                _settingsModel.IsFullScreenAppCheckEnabled = (bool)newValue;
                break;
            case nameof(AutoLaunchOnStartup) when (bool) newValue:
                _registryModel.AddAppStartupKey();
                break;
            case nameof(AutoLaunchOnStartup):
                _registryModel.DeleteAppStartupKey();
                break;
            case nameof(IsExtendedGammaRangeEnabled) when (bool)newValue:
                _registryModel.SetExtendedGammaRangeKey();
                break;
            case nameof(IsExtendedGammaRangeEnabled):
                _registryModel.SetDefaultGammaRangeKey();
                break;
            case nameof(IsGammaSmoothingEnabled):
                _settingsModel.IsGammaSmoothingEnabled = (bool)newValue;
                break;
        }
    }

    private void ApplicationCollectionChanged(object? sender, ApplicationCollectionChangedArgs args)
    {
        switch (args.Action)
        {
            case CollectionChangedAction.Added:
                Applications.Add(new ApplicationViewModel(args.App));
                break;
            case CollectionChangedAction.Removed:
                Applications.RemoveFirst(app => app.App.Name == args.App.Name);
                break;
            case CollectionChangedAction.Updated:
                break;
            default:
                throw new ArgumentOutOfRangeException(args.ToString());
        }
    }

    private void ScreensCollectionChanged(object? sender, ScreensCollectionChangedArgs args)
    {
        switch (args.Action)
        {
            case CollectionChangedAction.Added:
                Screens.Add(new ScreenViewModel(args.Screen));
                break;
            case CollectionChangedAction.Removed:
                Screens.RemoveFirst(screen => screen.Screen.DisplayCode == args.Screen.DisplayCode);
                break;
            case CollectionChangedAction.Updated:
                break;
            default:
                throw new ArgumentOutOfRangeException(args.ToString());
        }
    }
}