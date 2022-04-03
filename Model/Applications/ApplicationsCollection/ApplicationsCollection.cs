using System;
using System.Collections.Generic;
using System.ComponentModel;
using Common.Enums;
using Common.Extensions.CollectionChanged;
using Common.Infrastructure.INPC;
using Model.Entities;
using Model.Entities.Domain;

namespace Model.Applications.ApplicationsCollection
{
    internal partial class ApplicationsCollection : IReadOnlyDictionary<string, Application>
    {
        private readonly Dictionary<string, Application> _applications = new();

        public event EventHandler<ApplicationCollectionChangedArgs>? CollectionChanged;


        private void EntityPropertyChanged(object? sender, PropertyChangedEventArgs args)
        {
            if (sender is Application app)
            {
                CollectionChanged?.Invoke(this,
                    new ApplicationCollectionChangedArgs(app,
                        CollectionChangedAction.Updated,
                        args.PropertyName,
                        ((PropertyChangedValueEventArgs)args).NewValue));
            }
        }

    }
}
