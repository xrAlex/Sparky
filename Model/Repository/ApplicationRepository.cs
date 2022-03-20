using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Model.Repository
{
    internal class ApplicationRepository
    {
        [JsonProperty]
        private readonly List<string> _ignoredAppPaths = new();

        public void Add(string value)
        {
            if (!_ignoredAppPaths.Contains(value))
            {
                _ignoredAppPaths.Add(value);
            }
        }

        public bool Contains(string value) 
            => _ignoredAppPaths.Contains(value);

        public void Delete(string value)
        {
            if (Contains(value))
            {
                _ignoredAppPaths.Remove(value);
            }
        }

        public IReadOnlyList<string> GetData()
            => _ignoredAppPaths.ToList().AsReadOnly();
    }
}
