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
