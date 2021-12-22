using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Infrastructure.INPC;
using Common.Interfaces;

namespace ViewModel.Reflection
{
    public class ScreenVM : INPCBase
    {
        private IScreenContext _screen = null!;
        private bool _isSelected;

        public IScreenContext Screen
        {
            get => _screen;
            set => Set(ref _screen!, value);
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => Set(ref _isSelected, value);
        }

        public override string ToString()
            => _screen.FriendlyName;

        public ScreenVM(IScreenContext screen)
        {
           Screen = screen;
        }
    }
}
