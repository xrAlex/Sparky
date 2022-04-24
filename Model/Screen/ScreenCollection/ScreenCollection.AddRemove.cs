using Common.Enums;
using Common.Extensions.CollectionChanged;
using Model.Entities.Domain;

namespace Model.Screen.ScreenCollection;

internal sealed partial class ScreenCollection
{
    /// <summary>
    /// Добавление источника отображения в коллекцию.
    /// </summary>
    /// <param name="screen">Сформирвоанный объект класса ScreenContext.</param>
    public bool Add(ScreenContext screen)
    {
        if (ContainsKey(screen.DisplayCode))
        {
            return false;
        }

        _screens.Add(screen.DisplayCode, screen);

        screen.PropertyChanged += ScreenEntityChanged;

        CollectionChanged?.Invoke(this, new ScreensCollectionChangedArgs(screen, CollectionChangedAction.Added));
        return true;
    }

    /// <summary>
    /// Удаление источника отображения из коллекции.
    /// </summary>
    /// <param name="key">DisplayCode источника отображения.</param>
    public bool Remove(int key)
    {
        if (!TryGetValue(key, out var screen))
        {
            return false;
        }

        _screens.Remove(key);

        screen.PropertyChanged -= ScreenEntityChanged;

        CollectionChanged?.Invoke(this, new ScreensCollectionChangedArgs(screen, CollectionChangedAction.Removed));
        return true;
    }
}