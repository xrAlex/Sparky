﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Common.Interfaces;

namespace Model.Screen.ScreenCollection;

internal sealed partial class ScreenCollection
{
    public IScreenContext this[int key] 
        => _screens[key];

    public IEnumerable<int> Keys 
        => _screens.Keys;

    public void Clear()
    {
        foreach (var screen in _screens)
        {
            Remove(screen.Key);
        }
    }

    public IEnumerable<IScreenContext> Values 
        => _screens.Values;

    public int Count 
        => _screens.Count;

    public bool ContainsKey(int key) 
        => _screens.ContainsKey(key);

    public IEnumerator<KeyValuePair<int, IScreenContext>> GetEnumerator() 
        => _screens.GetEnumerator();

    public bool TryGetValue(int key, [MaybeNullWhen(false)] out IScreenContext value) 
        => _screens.TryGetValue(key, out value);

    IEnumerator IEnumerable.GetEnumerator() 
        => _screens.GetEnumerator();
}