using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
