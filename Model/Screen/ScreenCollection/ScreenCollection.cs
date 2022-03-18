using System;
using System.Collections.Generic;
using Common.Enums;
using Common.Extensions.CollectionChanged;
using Common.Interfaces;
using Model.Entities;

namespace Model.Screen.ScreenCollection
{
    internal sealed partial class ScreenCollection : IReadOnlyDictionary<int, IScreenContext>
    {
        private readonly Dictionary<int, IScreenContext> _screens = new();

        public event EventHandler<ScreensCollectionChangedArgs>? CollectionChanged;

        private void ScreenEntityChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is ScreenContext screen)
            {
                CollectionChanged?.Invoke(this, new ScreensCollectionChangedArgs(screen, CollectionChangedAction.Updated));
            }
        }
    }
}
