﻿using System;
using Common.Enums;
using Common.Extensions.CollectionChanged;

namespace Model.Applications
{
    internal partial class ApplicationModel
    {
        private event EventHandler<ApplicationCollectionChangedArgs>? InternalCollectionChanged;

        public event EventHandler<ApplicationCollectionChangedArgs>? ApplicationCollectionChanged
        {
            add
            {
                if (value == null) return;

                lock (_eventLocker)
                {
                    foreach (var application in _applications.Values)
                    {
                        value(this, new ApplicationCollectionChangedArgs(application.Name, CollectionChangedAction.Added));
                    }
                    InternalCollectionChanged += value;
                }
            }
            remove
            {
                lock (_eventLocker)
                {
                    InternalCollectionChanged -= value;
                }
            }
        }
    }
}
