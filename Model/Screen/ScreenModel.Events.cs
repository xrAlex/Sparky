using System;
using Common.Enums;
using Common.Extensions.CollectionChanged;
using Model.Entities.Domain;

namespace Model.Screen
{
    internal sealed partial class ScreenModel
    {
        private event EventHandler<ScreensCollectionChangedArgs>? InternalCollectionChanged;

        public event EventHandler<ScreensCollectionChangedArgs>? ScreensCollectionChanged
        {
            add
            {
                if (value == null) return;

                lock (_eventLocker)
                {
                    foreach (var screenContext in _screenCollection.Values)
                    {
                        value(this, new ScreensCollectionChangedArgs((ScreenContext)screenContext, CollectionChangedAction.Added));
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
