using System;
using Common.Infrastructure.Commands;
using Common.Infrastructure.ViewModelTemplate;
using Common.Interfaces;

namespace ViewModel.ViewModels;

public sealed class TrayViewModel : ViewModelBase
{
    private readonly IPeriodObserverModel _observer;
    private bool _isObserverWorking;

    /// <summary>
    /// Статус работы цикла PeriodObserverModel
    /// </summary>
    public bool IsObserverWorking
    {
        get => _isObserverWorking;
        private set => Set(ref _isObserverWorking, value);
    }

    public RelayCommand StartStopObserver { get; }
    public RelayCommand UnSubscribeEvents { get; }

    /// <summary>
    /// Остановка и запуск цикла PeriodObserverModel
    /// </summary>
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
        _observer.ObserverStarted += ObserverStarted;
        _observer.ObserverStopped += ObserverStopped;
    }

    public void UnSubscribeEventsExecute()
    {
        _observer.ObserverStarted -= ObserverStarted;
        _observer.ObserverStopped -= ObserverStopped;
    }

    private void ObserverStopped(object? sender, EventArgs e) 
        => IsObserverWorking = false;

    private void ObserverStarted(object? sender, EventArgs e) 
        => IsObserverWorking = true;
}