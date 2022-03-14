using System;
using System.Collections.ObjectModel;
using Common.Extensions;
using Common.Extensions.CollectionChanged;
using Common.Infrastructure.ViewModelTemplate;
using Common.Interfaces;
using ViewModel.Reflection;

namespace ViewModel.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly IScreenModel _screenModel;
        private ScreenVM _selectedScreen;
        public ObservableCollection<ScreenVM> Screens { get; } = new();

        public ScreenVM SelectedScreen
        {
            get => _selectedScreen;
            set => Set(ref _selectedScreen, value);
        }

        public SettingsViewModel(IScreenModel screenModel)
        {
            _screenModel = screenModel;
            screenModel.ScreensCollectionChanged += ScreensCollectionChanged;
        }

        private void ScreensCollectionChanged(object? sender, ScreensCollectionChangedArgs args)
        {
            switch (args.Action)
            {
                case CollectionChangedAction.Added:
                    Screens.Add(new ScreenVM(args.Screen));
                    break;
                case CollectionChangedAction.Removed:
                    Screens.RemoveFirst(screen => screen.Screen.DisplayCode == args.Screen.DisplayCode);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(args.ToString());
            }
        }
    }
}
