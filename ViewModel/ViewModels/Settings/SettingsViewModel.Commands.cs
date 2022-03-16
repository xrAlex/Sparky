using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Infrastructure.Commands;

namespace ViewModel.ViewModels.Settings
{
    public partial class SettingsViewModel
    {
        public RelayCommand RefreshApplicationsList { get; }

        private void RefreshApplicationsListExecute() 
            => _applicationModel.RefreshApplications();

    }
}
