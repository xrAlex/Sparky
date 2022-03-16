using System;
using Common.Enums;

namespace Common.Extensions.CollectionChanged
{
    public class ApplicationCollectionChangedArgs : EventArgs
    {
        public string Name { get; }

        public CollectionChangedAction Action { get; }

        public ApplicationCollectionChangedArgs(string name, CollectionChangedAction action)
        {
            Name = name;
            Action = action;
        }
    }
}
