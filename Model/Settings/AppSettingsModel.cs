using System.Collections.Generic;
using Common.Interfaces;
using Model.Repository;

namespace Model.Settings
{
    internal sealed partial class AppSettingsModel : IAppSettingsModel
    {
        private readonly string _filePath;
        public ScreenRepository ScreenRepository { get; } = new();
        public List<string> IgnoredApplications { get; } = new();

        public AppSettingsModel(string configurationFilePath)
        {
            _filePath = configurationFilePath;
        }
    }
}
