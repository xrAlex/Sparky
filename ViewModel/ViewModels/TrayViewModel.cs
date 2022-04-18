using System;
using Common.Infrastructure.Commands;
using Common.Infrastructure.ViewModelTemplate;
using Common.Interfaces;

namespace ViewModel.ViewModels;

public sealed class TrayViewModel : ViewModelBase
{
    private readonly IPeriodObserverModel _observer;
    private bool _isObserverWorking;

    public bool IsObserverWorking
    {
        get => _isObserverWorking;
        private set => Set(ref _isObserverWorking, value);
    }

    public RelayCommand StartStopObserver { get; }
    public RelayCommand UnSubscribeEvents { get; }

    private void StartStopObserverExecute()
    {
        if (IsObserverWorking)
        {
            _observer.StopWatch();
        }
        else
        {
            _observer.StartWatch();
        }
    }

    public TrayViewModel(IPeriodObserverModel observer)
    {
        _observer = observer;

        StartStopObserver = new RelayCommand(StartStopObserverExecute);
        UnSubscribeEvents = new RelayCommand(UnSubscribeEventsExecute);

        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        _observer.ObserverStarted += Observer_ObserverStarted;
        _observer.ObserverStopped += Observer_ObserverStopped;
    }

    public void UnSubscribeEventsExecute()
    {
        _observer.ObserverStarted -= Observer_ObserverStarted;
        _observer.ObserverStopped -= Observer_ObserverStopped;
    }

    private void Observer_ObserverStopped(object? sender, EventArgs e) 
        => IsObserverWorking = false;

    private void Observer_ObserverStarted(object? sender, EventArgs e) 
        => IsObserverWorking = true;
}