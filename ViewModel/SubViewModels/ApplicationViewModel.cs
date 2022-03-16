using Common.Infrastructure.INPC;

namespace ViewModel.SubViewModels
{
    public class ApplicationViewModel : INPCBase
    {
        private bool _isSelected;
        public string Name { get; }

        public bool IsSelected
        {
            get => _isSelected;
            set => Set(ref _isSelected, value);
        }

        public ApplicationViewModel(string name)
        {
            Name = name;
        }
    }
}
