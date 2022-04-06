using Common.Infrastructure;
using ViewModel.ViewModels;
using ViewModel.ViewModels.Settings;

namespace ViewModel;

public sealed class ViewModelLocator
{
    public static MainWindowViewModel MainWindowViewModel
        => IoC.GetInstance<MainWindowViewModel>();

    public static SettingsViewModel SettingsViewModel
        => IoC.GetInstance<SettingsViewModel>();
}