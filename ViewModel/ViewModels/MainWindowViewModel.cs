using System;
using System.Collections.ObjectModel;
using Common.Enums;
using Common.Extensions;
using Common.Extensions.CollectionChanged;
using Common.Infrastructure.ViewModelTemplate;
using Common.Interfaces;
using ViewModel.SubViewModels;

namespace ViewModel.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IScreenModel _screenModel;

        public ObservableCollection<ScreenViewModel> Screens { get; } = new();

        public MainWindowViewModel(IScreenModel screenModel)
        {
            _screenModel = screenModel;
            screenModel.ScreensCollectionChanged += ScreensCollectionChanged;
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
}
