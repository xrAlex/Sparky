using Common.Infrastructure.INPC;
using Common.Interfaces;

namespace ViewModel.SubViewModels
{
    public class ApplicationViewModel : INPCBase
    {
        public IApplication App { get; }

        private bool _isIgnored;

        public bool IsIgnored
        {
            get => _isIgnored;
            set => Set(ref _isIgnored, value);
        }

        protected override void OnPropertyChanged(in string propertyName, in object oldValue, in object newValue)
        {
            base.OnPropertyChanged(in propertyName, in oldValue, in newValue);
            if (propertyName == nameof(IsIgnored))
            {
                App.IsIgnored = IsIgnored;
            }
        }

        public ApplicationViewModel(IApplication app)
        {
            App = app;
            _isIgnored = app.IsIgnored;
        }
    }
}
