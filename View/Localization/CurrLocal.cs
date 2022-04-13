using System.Windows;

namespace View.Localization
{
    public class CurrLocal : LocalBinding
    {
        private static readonly string current = nameof(Localizator.Current);

        public string CurrPath
        {
            get
            {
                if (Path == null)
                    Path = new PropertyPath(string.Empty);
                string path = Path.Path;
                string currPath = string.Empty;

                if (string.IsNullOrWhiteSpace(path))
                {
                    path = current;
                }
                else if (path.StartsWith(current))
                {
                    if (!Path.Equals(current))
                    {
                        if (path[current.Length] == '.')
                        {
                            currPath = path.Substring(current.Length + 1);
                        }
                        else
                        {
                            currPath = path;
                            path = $"{current}.{path}";
                        }
                    }
                }
                else
                {
                    currPath = path;
                    path = $"{current}.{path}";
                }

                Path.Path = path;
                return currPath;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    Path.Path = current;
                else
                    Path.Path = $"{current}.{value}";
            }
        }

        public CurrLocal()
        {
            Path = new PropertyPath(current);
        }

        public CurrLocal(string path) : base(path)
        {
            Path = new PropertyPath($"{current}.{path}");
        }
    }
}
