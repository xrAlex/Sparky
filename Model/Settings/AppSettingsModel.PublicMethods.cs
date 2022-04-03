using System;
using System.IO;
using System.Threading.Tasks;
using Common.Interfaces;
using Newtonsoft.Json;

namespace Model.Settings
{
    internal partial class AppSettingsModel
    {
        /// <inheritdoc cref="IAppSettingsModel.SaveAsync"/>
        public Task SaveAsync()
            => Task.Run(Save);

        /// <inheritdoc cref="IAppSettingsModel.LoadAsync"/>
        public Task LoadAsync()
            => Task.Run(Load);

        /// <inheritdoc cref="IAppSettingsModel.ResetAsync"/>
        public Task ResetAsync()
            => Task.Run(Reset);

        public void Reset()
        {
            SettingsReset?.Invoke(this, EventArgs.Empty);
            ScreenRepository.Clear();
            IgnoredAppRepository.Clear();
            Load();
        }

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
                // TODO: ошибка сохранения настроек
            }
            finally
            {
                SettingsSaved?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <inheritdoc cref="IAppSettingsModel.Load"/>
        public void Load()
        {
            try
            {
                var serializer = new JsonSerializer();
                using var fs = File.Open(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var reader = new StreamReader(fs);

                serializer.Populate(reader, this);
            }
            catch (Exception)
            {
                // TODO: ошибка загрузки настроек
            }
            finally
            {
                SettingsLoaded?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
