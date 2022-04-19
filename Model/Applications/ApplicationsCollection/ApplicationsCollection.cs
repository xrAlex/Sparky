using System;
using System.Collections.Generic;
using System.ComponentModel;
using Common.Enums;
using Common.Extensions.CollectionChanged;
using Common.Infrastructure.INPC;
using Model.Entities.Domain;

namespace Model.Applications.ApplicationsCollection;

internal sealed partial class ApplicationsCollection : IReadOnlyDictionary<string, Application>
{
    private readonly Dictionary<string, Application> _applications = new();
    public event EventHandler<ApplicationCollectionChangedArgs>? CollectionChanged;

    private void EntityPropertyChanged(object? sender, PropertyChangedEventArgs args)
    {
        CollectionChanged?.Invoke(this,
            new ApplicationCollectionChangedArgs((Application)sender!,
                CollectionChangedAction.Updated,
                args.PropertyName!,
                ((PropertyChangedValueEventArgs)args).NewValue));
    }
}