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
        List<string> GetAllNames();

        /// <summary>
        /// Помечает приложение как игнорируемое пользователем.
        /// </summary>
        /// <param name="appName">Имя приложения.</param>
        /// <param name="value">Значение параметра.</param>
        void SetIgnored(string appName, bool value);

        /// <summary>
        /// Проверяет игнорируется ли приложение.
        /// </summary>
        /// <param name="appName">Имя приложения.</param>
        bool IsAppIgnored(string appName);

        /// <summary>
        /// Событие оповещающее об изменении внутренней коллекции приложений.
        /// </summary>
        /// <remarks>При подписке на событие возвращает все элементы коллекции.</remarks>
        event EventHandler<ApplicationCollectionChangedArgs>? ApplicationCollectionChanged;
    }
}
