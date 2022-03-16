using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;

namespace Model.Applications.ApplicationsCollection
{
    internal partial class ApplicationsCollection
    {
        public IEnumerator<KeyValuePair<string, Application>> GetEnumerator() 
            => _applications.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() 
            => ((IEnumerable) _applications).GetEnumerator();

        public int Count 
            => _applications.Count;

        public void Clear()
        {
            foreach (var applicationName in _applications.Keys)
            {
                Remove(applicationName);
            }
        }

        public bool ContainsKey(string key) 
            => _applications.ContainsKey(key);

        public bool TryGetValue(string key, out Application value) 
            => _applications.TryGetValue(key, out value);

        public Application this[string key] 
            => _applications[key];

        public IEnumerable<string> Keys 
            => ((IReadOnlyDictionary<string, Application>) _applications).Keys;

        public IEnumerable<Application> Values 
            => ((IReadOnlyDictionary<string, Application>) _applications).Values;
    }
}
