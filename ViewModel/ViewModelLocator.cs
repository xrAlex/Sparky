using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Infrastructure;
using ViewModel.ViewModels;

namespace ViewModel
{
    public sealed class ViewModelLocator
    {
        public static MainWindowViewModel MainWindowViewModel
            => IoCKernel.IoC.GetInstance<MainWindowViewModel>();
    }
}
