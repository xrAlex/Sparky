using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;
using Model.Entities;
using Newtonsoft.Json;

namespace Model.Settings
{
    public sealed class AppSettingsModel
    {
        internal Dictionary<int, ScreenSettings> Screens = new();

        private const string FilePath = ".\\config.json";

        public void Save()
        {
            var serializer = new JsonSerializer();
            using var file = File.CreateText(FilePath);
            serializer.Formatting = Formatting.Indented;

            serializer.Serialize(file, Screens);
        }

        public void Load()
        {
            var serializer = new JsonSerializer();
            using var file = File.OpenText(FilePath);

            Screens = serializer
                          .Deserialize(file, typeof(Dictionary<int, ScreenSettings>)) as Dictionary<int, ScreenSettings> 
                      ?? throw new InvalidOperationException();
        }
    }
}
