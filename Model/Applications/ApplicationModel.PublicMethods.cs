using System.Collections.Generic;
using System.Linq;

namespace Model.Applications
{
    internal partial class ApplicationModel
    {
        //public void MoveToIgnored(List<string> applicationsName)
        //{
        //    // TODO: Сперва нужно скопировать имя в модель настроек
        //    foreach (var name in applicationsName.Where(name => _applications.ContainsKey(name)))
        //    {
        //        _applications.Remove(name);
        //    }
        //}

        public void RefreshApplications()
        {
            _applications.Clear();
            FillApplicationsCollection();
        }

        public List<string> GetAllNames() 
            => _applications.Select(application => application.Value.Name).ToList();
        
    }
}
