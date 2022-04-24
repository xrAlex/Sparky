using Common.Enums;
using Common.Extensions.CollectionChanged;
using Model.Entities.Domain;

namespace Model.Applications.ApplicationsCollection;

internal sealed partial class ApplicationsCollection
{
    /// <summary>
    /// Добавляет приложение в коллеккцию.
    /// </summary>
    /// <param name="application">Сформированная сущность приложения.</param>
    public bool Add(Application application)
    {
        if (ContainsKey(application.Name))
        {
            return false;
        }

        _applications.Add(application.Name, application);

        application.PropertyChanged += EntityPropertyChanged;

        CollectionChanged?.Invoke(this, new ApplicationCollectionChangedArgs(application, CollectionChangedAction.Added));
        return true;
    }

    /// <summary>
    /// Удаление приложения из коллекции.
    /// </summary>
    /// <param name="key">Имя приложения</param>
    public bool Remove(string key)
    {
        if (!TryGetValue(key, out var app))
        {
            return false;
        }

        _applications.Remove(key);

        CollectionChanged?.Invoke(this, new ApplicationCollectionChangedArgs(app, CollectionChangedAction.Removed));
        return true;
    }
}