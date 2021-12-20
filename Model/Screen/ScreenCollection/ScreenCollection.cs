using System;
using System.Collections.Generic;
using Common.Extensions.CollectionChanged;
using Common.Interfaces;

namespace Model.Screen.ScreenCollection
{
    internal sealed partial class ScreenCollection : IReadOnlyDictionary<int, IScreenContext>
    {
        private readonly Dictionary<int, IScreenContext> _screens = new();

        public event EventHandler<ScreensCollectionChangedArgs>? CollectionChanged;
    }
}
