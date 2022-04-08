using System;
using System.Collections.ObjectModel;
using System.Net.Mime;
using System.Windows;
using Common.Enums;
using Common.Extensions;
using Common.Extensions.CollectionChanged;
using Common.Infrastructure.Commands;
using Common.Infrastructure.ViewModelTemplate;
using Common.Interfaces;
using ViewModel.SubViewModels;

namespace ViewModel.ViewModels;

public sealed class MainWindowViewModel : ViewModelBase
{
    private readonly IScreenModel _screenModel;
    private readonly IPeriodObserverModel _periodObserverModel;
    public ObservableCollection<ScreenViewModel> Screens { get; } = new();

    public RelayCommand StopObserver { get; }
    public RelayCommand UnsubscribeEvents { get; }

    private void StopObserverExecute()
    {
        _periodObserverModel.StopWatch();
    }

    public MainWindowViewModel(IScreenModel screenModel, IPeriodObserverModel periodObserverModel)
    {
        _screenModel = screenModel;
        _periodObserverModel = periodObserverModel;

        StopObserver = new RelayCommand(StopObserverExecute);
        UnsubscribeEvents = new RelayCommand(UnsubscribeEventsExecute);

        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        _screenModel.ScreensCollectionChanged += ScreensCollectionChanged;
    }

    private void UnsubscribeEventsExecute()
    {
        _screenModel.ScreensCollectionChanged -= ScreensCollectionChanged;
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
                throw new ArgumentOutOfRangeException($"Screens collection unknown action {args.Action}");
        }
    }
}