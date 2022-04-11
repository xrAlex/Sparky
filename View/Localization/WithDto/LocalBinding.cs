using System.Windows.Data;

namespace View.Localization.WithDto
{
    public class LocalBinding : Binding
    {
        public LocalBinding()
        {
            Source = Localizator.Instance;
        }

        public LocalBinding(string path)
            : base(path)
        {
            Source = Localizator.Instance;
        }
    }
}
