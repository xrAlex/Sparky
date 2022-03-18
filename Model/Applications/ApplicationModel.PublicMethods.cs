using System.Collections.Generic;
using System.Linq;
using Common.Interfaces;

namespace Model.Applications
{
    internal partial class ApplicationModel
    {
        /// <inheritdoc cref="IApplicationModel.RefreshApplications"/>
        public void RefreshApplications()
        {
            _applications.Clear();
            FillApplicationsCollection();
        }

        /// <inheritdoc cref="IApplicationModel.GetAllNames"/>
        public List<string> GetAllNames() 
            => _applications.Select(application => application.Value.Name).ToList();

        public void SetIgnored(string appName, bool value)
        {
            if (_applications.ContainsKey(appName))
            {
                _applications[appName].IsIgnored = value;
            }
        }

        public bool IsAppIgnored(string appName) 
            => _applications.ContainsKey(appName) && _applications[appName].IsIgnored;
    }
}
