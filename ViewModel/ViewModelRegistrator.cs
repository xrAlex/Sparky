using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using ViewModel.ViewModels;

namespace ViewModel
{
    public class ViewModelRegistrator
    {
        public static void Register(Container container)
        {
            container.Register<MainWindowViewModel>(Lifestyle.Transient);
        }
    }
}
