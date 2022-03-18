using Common.Interfaces;

namespace Model.Applications
{
    internal partial class ApplicationModel : IApplicationModel
    {
        private readonly ApplicationsCollection.ApplicationsCollection _applications = new();
        private readonly object _eventLocker = new();
        private readonly IScreenModel _screenModel;
        private readonly IAppSettingsModel _appSettings;

        public ApplicationModel(IScreenModel screenModel, IAppSettingsModel appSettings)
        {
            _screenModel = screenModel;
            _appSettings = appSettings;
            _appSettings.SettingsLoaded += SettingsLoaded;
            _appSettings.SettingsReset += SettingsReset;
            _applications.CollectionChanged += CollectionChanged;
        }

        private void SettingsReset(object? sender, System.EventArgs e)
        {
            _applications.Clear();
        }

        private void SettingsLoaded(object? sender, System.EventArgs e)
        {
            FillApplicationsCollection();
        }
    }
}
