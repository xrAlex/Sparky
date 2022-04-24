using System;
using Common.Enums;
using Common.Interfaces;

namespace Common.Extensions.CollectionChanged;

public sealed class ApplicationCollectionChangedArgs : EventArgs
{
    public IApplication App { get; }
    public CollectionChangedAction Action { get; }
    public string? PropertyName { get; }
    public object? NewValue { get; }

    public override string ToString()
        => $"Screen: {App.Name}, Action: {Action}";

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