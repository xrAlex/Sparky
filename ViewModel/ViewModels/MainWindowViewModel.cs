using System;
using System.Collections.ObjectModel;
using Common.Enums;
using Common.Extensions;
using Common.Extensions.CollectionChanged;
using Common.Infrastructure.Commands;
using Common.Infrastructure.ViewModelTemplate;
using Common.Interfaces;

namespace ViewModel.ViewModels;

public sealed class MainWindowViewModel : ViewModelBase
{
    private readonly IScreenModel _screenModel;
    private readonly IPeriodObserverModel _periodObserverModel;
    public ObservableCollection<IScreenContext> Screens { get; } = new();

    private bool _isObserverWorking;
    private IScreenContext _selectedScreen;

    public IScreenContext? SelectedScreen
    {
        get => _selectedScreen;
        set => Set(ref _selectedScreen, value);
    }

    public bool IsObserverWorking
    {
        get => _isObserverWorking;
        private set => Set(ref _isObserverWorking, value);
    }

    private void StartStopObserverExecute()
    {
        if (IsObserverWorking)
        {
            _periodObserverModel.StopWatch();
        }
        else
        {
            _periodObserverModel.StartWatch();
        }
    }

    public RelayCommand StartStopObserver { get; }
    public RelayCommand UnsubscribeEvents { get; }

    public MainWindowViewModel(IScreenModel screenModel, IPeriodObserverModel periodObserverModel)
    {
        _screenModel = screenModel;
        _periodObserverModel = periodObserverModel;

        StartStopObserver = new RelayCommand(StartStopObserverExecute);
        UnsubscribeEvents = new RelayCommand(UnsubscribeEventsExecute);

        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        _screenModel.ScreensCollectionChanged += ScreensCollectionChanged;
        _periodObserverModel.ObserverStarted += ObserverStarted;
        _periodObserverModel.ObserverStopped += ObserverStopped;
    }

    private void UnsubscribeEventsExecute()
    {
        _screenModel.ScreensCollectionChanged -= ScreensCollectionChanged;
        _periodObserverModel.ObserverStarted -= ObserverStarted;
        _periodObserverModel.ObserverStopped -= ObserverStopped;
    }

    private void ObserverStopped(object? sender, EventArgs e)
        => IsObserverWorking = false;

    private void ObserverStarted(object? sender, EventArgs e)
        => IsObserverWorking = true;

    private void ScreensCollectionChanged(object? sender, ScreensCollectionChangedArgs args)
    {
        switch (args.Action)
        {
            case CollectionChangedAction.Added:
                Screens.Add(args.Screen);
                SelectedScreen ??= Screens[0];
                break;
            case CollectionChangedAction.Removed:
                Screens.RemoveFirst(screen => screen.DisplayCode == args.Screen.DisplayCode);
                break;
            case CollectionChangedAction.Updated:
                break;
            default:
                throw new ArgumentOutOfRangeException($"Screens collection unknown action {args.Action}");
        }
    }
}