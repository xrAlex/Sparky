using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Model.Entities;
using Newtonsoft.Json;

namespace Model.Settings
{
    internal sealed class AppSettingsModel : IAppSettingsModel
    {
        [JsonProperty]
        internal Dictionary<int, ScreenSettings> Screens = new();

        private readonly string _filePath;

        /// <inheritdoc cref="IAppSettingsModel.SaveAsync"/>
        public Task SaveAsync()
            => Task.Run(Save);

        /// <inheritdoc cref="IAppSettingsModel.LoadAsync"/>
        public Task LoadAsync()
            => Task.Run(Load);

        /// <inheritdoc cref="IAppSettingsModel.Save"/>
        public void Save()
        {
            try
            {
                var serializer = new JsonSerializer()
                {
                    Formatting = Formatting.Indented,
                };

                using var fs = File.Open(_filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                using var writer = new StreamWriter(fs);

                serializer.Serialize(writer, this);
            }
            catch (Exception)
            {
                // ошибка сохранения настроек
            }
        }

        /// <inheritdoc cref="IAppSettingsModel.Load"/>
        public void Load()
        {
            if (!File.Exists(_filePath)) return;

            try
            {
                var serializer = new JsonSerializer();
                using var fs = File.Open(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var reader = new StreamReader(fs);

                serializer.Populate(reader, this);
            }
            catch (Exception)
            {
                // ошибка загрузки настроек
            }
        }

        public AppSettingsModel(string configurationFilePath)
        {
            _filePath = configurationFilePath;
        }
    }
}
