using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces;
using Model.Entities;
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
