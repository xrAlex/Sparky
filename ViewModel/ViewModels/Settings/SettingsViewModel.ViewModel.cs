using System;
using System.Collections.ObjectModel;
using Common.Enums;
using Common.Extensions;
using Common.Extensions.CollectionChanged;
using ViewModel.SubViewModels;

namespace ViewModel.ViewModels.Settings
{
    public partial class SettingsViewModel
    {
        public ObservableCollection<ScreenViewModel> Screens { get; } = new();
        public ObservableCollection<ApplicationViewModel> Applications { get; } = new();
        private bool _checkFullScreensApps;
        private ScreenViewModel _selectedScreen;

        public bool CheckFullScreensApps
        {
            get => _checkFullScreensApps;
            set => Set(ref _checkFullScreensApps, value);
        }

        public ScreenViewModel SelectedScreen
        {
            get => _selectedScreen;
            set => Set(ref _selectedScreen, value);
        }

        private void ApplicationCollectionChanged(object? sender, ApplicationCollectionChangedArgs args)
        {
            switch (args.Action)
            {
                case CollectionChangedAction.Added:
                    Applications.Add(new ApplicationViewModel(args.Name));
                    break;
                case CollectionChangedAction.Removed:
                    Applications.RemoveFirst(app => app.Name == args.Name);
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
}
