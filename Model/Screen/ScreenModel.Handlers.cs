using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions.CollectionChanged;
using Model.Entities;

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
