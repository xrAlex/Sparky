using System;
using System.Collections.Generic;
using System.ComponentModel;
using Common.Enums;
using Common.Extensions.CollectionChanged;
using Common.Infrastructure.INPC;
using Common.Interfaces;
using Model.Entities.Domain;

namespace Model.Screen.ScreenCollection;

internal sealed partial class ScreenCollection : IReadOnlyDictionary<int, IScreenContext>
{
    private readonly Dictionary<int, IScreenContext> _screens = new();

    public event EventHandler<ScreensCollectionChangedArgs>? CollectionChanged;

    private void ScreenEntityChanged(object? sender, PropertyChangedEventArgs args)
    {
        if (sender is ScreenContext screen)
        {
            CollectionChanged?.Invoke(this,
                new ScreensCollectionChangedArgs(screen,
                    CollectionChangedAction.Updated,
                    args.PropertyName!,
                    ((PropertyChangedValueEventArgs) args).NewValue));
        }
    }
}