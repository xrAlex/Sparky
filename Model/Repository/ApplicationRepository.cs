using System.Collections.Generic;
using Newtonsoft.Json;

namespace Model.Repository;

internal sealed class ApplicationRepository
{
    [JsonProperty("Applications path")]
    private readonly List<string> _ignoredAppPaths = new();

    public void Add(string value)
    {
        if (!Contains(value))
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

    public void Clear()
        => _ignoredAppPaths.Clear();
        

    public IEnumerable<string> GetData()
        => _ignoredAppPaths.AsReadOnly();
}