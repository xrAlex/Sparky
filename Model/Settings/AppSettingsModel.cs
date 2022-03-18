using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Common.Interfaces;
using Model.Entities;
using Model.Repository;
using Model.Screen.ScreenCollection;
using Newtonsoft.Json;

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
