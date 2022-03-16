using System;
using System.Collections.Generic;
using Common.Extensions.CollectionChanged;
using Model.Entities;

namespace Model.Applications.ApplicationsCollection
{
    internal partial class ApplicationsCollection : IReadOnlyDictionary<string, Application>
    {
        private readonly Dictionary<string, Application> _applications = new();

        public event EventHandler<ApplicationCollectionChangedArgs>? CollectionChanged;
    }
}
