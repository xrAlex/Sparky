using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Model.Entities;
using Newtonsoft.Json;

namespace Model.Settings
{
    public sealed class AppSettingsModel
    {
        [JsonProperty]
        internal Dictionary<int, ScreenSettings> Screens = new();

        private readonly string _filePath = ".\\config.json";

        public Task SaveAsync() 
            => Task.Run(Save);
        public Task LoadAsync() 
            => Task.Run(Load);

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
            catch (Exception ex)
            {
                // ошибка сохранения настроек
            }
        }

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
            catch (Exception ex)
            {
                // ошибка загрузки настроек
            }
        }

        public AppSettingsModel(string? settingsFilePath = null) 
            => _filePath = settingsFilePath ?? _filePath;
    }
}
