using System;
using Common.Enums;
using Common.Interfaces;

namespace Common.Extensions.CollectionChanged
{
    public class ApplicationCollectionChangedArgs : EventArgs
    {
        public IApplication App { get; }
        public CollectionChangedAction Action { get; }

        public ApplicationCollectionChangedArgs(IApplication app, CollectionChangedAction action)
        {
            App = app;
            Action = action;
        }
    }
}
