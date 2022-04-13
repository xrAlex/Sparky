using System.Windows.Data;

namespace View.Localization
{
    public class LocalBinding : Binding
    {
        private static readonly LocalizatorApp source = new LocalizatorApp();
        public LocalBinding()
        {
            Source = source;
        }

        public LocalBinding(string path) : base(path)
        {
            Source = source;
        }
    }
}
