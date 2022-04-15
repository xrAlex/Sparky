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
                Path ??= new(string.Empty);
                if (string.IsNullOrWhiteSpace(value))
                    Path.Path = current;
                else
                    Path.Path = $"{current}.{value}";
            }
        }

        public CurrLocal()
        {
            CurrPath = string.Empty;
        }

        public CurrLocal(string currPath)
            : base($"{current}.{currPath}")
        {
            CurrPath = currPath;
        }
    }
}
