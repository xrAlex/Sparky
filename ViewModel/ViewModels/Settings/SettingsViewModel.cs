using System;
using System.Collections.ObjectModel;
using Common.Enums;
using Common.Extensions;
using Common.Extensions.CollectionChanged;
using Common.Infrastructure.Commands;
using Common.Infrastructure.ViewModelTemplate;
using Common.Interfaces;
using ViewModel.SubViewModels;

namespace ViewModel.ViewModels.Settings
{
    public partial class SettingsViewModel : ViewModelBase
    {
        private readonly IScreenModel _screenModel;
        private readonly IApplicationModel _applicationModel;

        public SettingsViewModel(IScreenModel screenModel, IApplicationModel applicationModel)
        {
            _screenModel = screenModel;
            _applicationModel = applicationModel;
            RefreshApplicationsList = new RelayCommand(RefreshApplicationsListExecute);

            screenModel.ScreensCollectionChanged += ScreensCollectionChanged;
            applicationModel.ApplicationCollectionChanged += ApplicationCollectionChanged;
        }
    }
}
