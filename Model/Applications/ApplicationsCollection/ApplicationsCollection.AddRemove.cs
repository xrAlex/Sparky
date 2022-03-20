using Common.Enums;
using Common.Extensions.CollectionChanged;
using Model.Entities;

namespace Model.Applications.ApplicationsCollection
{
    internal partial class ApplicationsCollection
    {
        /// <summary>
        /// Создание источника отображения на основе DTO и добавление его в коллекцию.
        /// </summary>
        /// <param name="application">Сформированная сущность приложения.</param>
        /// <returns>Результат выполнения операции.</returns>
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
        /// <returns>Результат выполнения операции.</returns>
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
}
