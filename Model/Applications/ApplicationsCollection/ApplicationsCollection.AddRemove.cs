using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;
using Common.Enums;
using Common.Extensions.CollectionChanged;
using Model.Entities;

namespace Model.Applications.ApplicationsCollection
{
    internal partial class ApplicationsCollection
    {
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
