using System.Collections.Generic;
using System.Linq;
using Common.Interfaces;

namespace Model.Applications;

internal partial class ApplicationModel
{
    /// <inheritdoc cref="IApplicationModel.RefreshApplications"/>
    public void RefreshApplications()
    {
        _applications.Clear();
        FillApplicationsCollection();
    }

    /// <inheritdoc cref="IApplicationModel.GetAllNames"/>
    public IEnumerable<string> GetAllNames()
        => _applications
            .Select(application => application.Value.Name)
            .ToArray();

}