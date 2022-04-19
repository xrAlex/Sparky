using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Model.Entities.Domain;

namespace Model.Applications.ApplicationsCollection;

internal sealed partial class ApplicationsCollection
{
    public IEnumerator<KeyValuePair<string, Application>> GetEnumerator() 
        => _applications.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() 
        => ((IEnumerable) _applications).GetEnumerator();

    public int Count 
        => _applications.Count;

    /// <summary>
    /// Очистка коллекции с уведомлением о удалении элементов.
    /// </summary>
    public void Clear()
    {
        foreach (var applicationKey in _applications.Keys)
        {
            Remove(applicationKey);
        }
    }

    public bool ContainsKey(string key) 
        => _applications.ContainsKey(key);

    public bool TryGetValue(string key, [MaybeNullWhen(false)] out Application value) 
        => _applications.TryGetValue(key, out value);

    public Application this[string key] 
        => _applications[key];

    public IEnumerable<string> Keys 
        => ((IReadOnlyDictionary<string, Application>) _applications).Keys;

    public IEnumerable<Application> Values 
        => ((IReadOnlyDictionary<string, Application>) _applications).Values;
}