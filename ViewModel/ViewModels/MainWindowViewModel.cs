using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Extensions.CollectionChanged;
using Common.Infrastructure;
using Common.Infrastructure.ViewModelTemplate;
using Common.Interfaces;
using ViewModel.Reflection;

namespace ViewModel.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IScreenModel _screenModel;

        public ObservableCollection<ScreenVM> Screens { get; } = new();

        public MainWindowViewModel(IScreenModel screenModel)
        {
            _screenModel = screenModel;

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
