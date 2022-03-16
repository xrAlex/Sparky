﻿using Common.DTO;
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
        /// <param name="applicationDTO">Data Transfer Object класа приложенияю</param>
        /// <returns>Результат выполнения операции.</returns>
        public bool Add(ApplicationDTO applicationDTO)
        {
            if (ContainsKey(applicationDTO.Name))
            {
                return false;
            }

            //if (_applications.All(app => app.Value.ExecutableFilePath != applicationDTO.ExecutableFilePath))
            //{
            //    return false;
            //}

            var application = new Application(applicationDTO.Name)
            {
                ExecutableFilePath = applicationDTO.ExecutableFilePath
            };

            _applications.Add(application.Name, application);

            CollectionChanged?.Invoke(this, new ApplicationCollectionChangedArgs(application.Name, CollectionChangedAction.Added));
            return true;
        }
        /// <summary>
        /// Удаление приложения из коллекции.
        /// </summary>
        /// <param name="key">Имя приложения</param>
        /// <returns>Результат выполнения операции.</returns>
        public bool Remove(string key)
        {
            if (!TryGetValue(key, out var application))
            {
                return false;
            }

            _applications.Remove(key);

            CollectionChanged?.Invoke(this, new ApplicationCollectionChangedArgs(application.Name, CollectionChangedAction.Removed));
            return true;
        }
    }
}
