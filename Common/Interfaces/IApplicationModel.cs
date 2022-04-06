using System;
using System.Collections.Generic;
using Common.Extensions.CollectionChanged;

namespace Common.Interfaces
{
    public interface IApplicationModel
    {
        /// <summary>
        /// Обновляет внутреннию коллекцию приложений.
        /// </summary>
        void RefreshApplications();

        /// <returns>Коллекцию имен приложений.</returns>
        IReadOnlyList<string> GetAllNames();

        /// <summary>
        /// Событие оповещающее об изменении внутренней коллекции приложений.
        /// </summary>
        /// <remarks>При подписке на событие возвращает все элементы коллекции.</remarks>
        event EventHandler<ApplicationCollectionChangedArgs>? ApplicationCollectionChanged;
    }
}
