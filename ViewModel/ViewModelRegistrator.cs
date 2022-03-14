using SimpleInjector;
using ViewModel.ViewModels;

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
