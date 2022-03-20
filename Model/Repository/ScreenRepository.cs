using Common.Interfaces;
using Model.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Model.Repository
{
    /// <summary>
    /// Хранилище данных ScreenModel
    /// </summary>
    internal class ScreenRepository
    {
        [JsonProperty]
        private readonly Dictionary<int, ScreenContext> _repository = new();

        public bool TryAdd(int key, ScreenContext value)
            => _repository.TryAdd(key, value);
        
        public bool TryGetValue(int key, [MaybeNullWhen(false)] out ScreenContext value)
            => _repository.TryGetValue(key, out value);

        public bool TryUpdate(int key, ScreenContext value)
        {
            if (_repository.ContainsKey(key))
            {
                _repository[key] = value;
                return true;
            }
            return false;
        }

        public void Delete(int key) 
            => _repository.Remove(key);

        public void Clear()
            => _repository.Clear();

        public IReadOnlyDictionary<int, IScreenContext> GetData() 
            => _repository.ToDictionary(key => key.Key, value => (IScreenContext)value.Value);
    }
}
