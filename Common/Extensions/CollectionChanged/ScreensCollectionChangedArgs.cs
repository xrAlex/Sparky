using System;
using Common.Enums;
using Common.Interfaces;

namespace Common.Extensions.CollectionChanged;

public class ScreensCollectionChangedArgs : EventArgs
{
    public IScreenContext Screen { get; }
    public CollectionChangedAction Action { get; }
    public string? PropertyName { get; }
    public object? NewValue { get; }

    public override string ToString()
        => $"Screen: {Screen.DisplayCode}, Action: {Action}";

    public ScreensCollectionChangedArgs(IScreenContext screen, CollectionChangedAction action, string propertyName, object newValue)
    {
        Action = action;
        Screen = screen;
        PropertyName = propertyName;
        NewValue = newValue;
    }

    public ScreensCollectionChangedArgs(IScreenContext screen, CollectionChangedAction action)
    {
        Action = action;
        Screen = screen;
    }
}