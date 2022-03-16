using SimpleInjector;
using ViewModel.ViewModels;
using ViewModel.ViewModels.Settings;

namespace ViewModel
{
    public class ViewModelRegistrator
    {
        public static void Register(Container container)
        {
            container.Register<MainWindowViewModel>(Lifestyle.Transient);
            container.Register<SettingsViewModel>(Lifestyle.Transient);
        }
    }
}
