using Common.Interfaces;
using Model.Settings;

namespace Model.Applications;

internal sealed partial class ApplicationModel : IApplicationModel
{
    private readonly ApplicationsCollection.ApplicationsCollection _applications = new();
    private readonly object _eventLocker = new();
    private readonly AppSettingsModel _appSettings;
    private readonly IScreenModel _screenModel;

    public ApplicationModel(IAppSettingsModel appSettings, IScreenModel screenModel)
    {
        _appSettings = (AppSettingsModel) appSettings;
        _screenModel = screenModel;
        _appSettings.SettingsLoaded += SettingsLoaded;
        _appSettings.SettingsReset += SettingsReset;
        _applications.CollectionChanged += CollectionChanged;
    }

    private void SettingsReset(object? sender, System.EventArgs e) 
        => _applications.Clear();

    private void SettingsLoaded(object? sender, System.EventArgs e) 
        => FillApplicationsCollection();
}