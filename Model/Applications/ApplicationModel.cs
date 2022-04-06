using Common.Interfaces;
using Model.Settings;

namespace Model.Applications;

internal sealed partial class ApplicationModel : IApplicationModel
{
    private readonly ApplicationsCollection.ApplicationsCollection _applications = new();
    private readonly object _eventLocker = new();
    private readonly AppSettingsModel _appSettings;

    public ApplicationModel(IAppSettingsModel appSettings)
    {
        _appSettings = (AppSettingsModel) appSettings;
        _appSettings.SettingsLoaded += SettingsLoaded;
        _appSettings.SettingsReset += SettingsReset;
        _applications.CollectionChanged += CollectionChanged;
    }

    private void SettingsReset(object? sender, System.EventArgs e) 
        => _applications.Clear();

    private void SettingsLoaded(object? sender, System.EventArgs e) 
        => FillApplicationsCollection();
}