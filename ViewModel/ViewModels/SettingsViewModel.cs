using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;
using Common.Extensions;
using Common.Extensions.CollectionChanged;
using Common.Infrastructure.Commands;
using Common.Infrastructure.ViewModelTemplate;
using Common.Interfaces;
using ViewModel.Reflection;

namespace ViewModel.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private Pages _currentPage = Pages.MainPage;
        private readonly IScreenModel _screenModel;

        public Pages CurrentPage
        {
            get => _currentPage;
            set => Set(ref _currentPage, value);
        }

        public ObservableCollection<ScreenVM> Screens { get; } = new();

        public RelayCommand<Pages> SetPage { get; }

        private void SetPageExecute(Pages page)
        {
            CurrentPage = page;
        }

        private bool SetPageCanExecute(Pages page) 
            => CurrentPage != page;

        public SettingsViewModel(IScreenModel screenModel)
        {
            _screenModel = screenModel;
            SetPage = new RelayCommand<Pages>(SetPageExecute, SetPageCanExecute);

            screenModel.ScreensCollectionChanged += ScreensCollectionChanged;
            var asd = Screens;
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
