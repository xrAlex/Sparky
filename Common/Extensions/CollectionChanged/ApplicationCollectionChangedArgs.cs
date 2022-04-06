using System;
using Common.Enums;
using Common.Interfaces;

namespace Common.Extensions.CollectionChanged;

public class ApplicationCollectionChangedArgs : EventArgs
{
    public IApplication App { get; }
    public CollectionChangedAction Action { get; }
    public string? PropertyName { get; }
    public object? NewValue { get; }

    public ApplicationCollectionChangedArgs(IApplication app, CollectionChangedAction action, string propertyName, object newValue)
    {
        App = app;
        Action = action;
        PropertyName = propertyName;
        NewValue = newValue;
    }

    public ApplicationCollectionChangedArgs(IApplication app, CollectionChangedAction action)
    {
        App = app;
        Action = action;
    }
}