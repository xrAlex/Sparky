using System;
using Common.Interfaces;

namespace Model.Settings
{
    internal partial class AppSettingsModel
    {
        /// <inheritdoc cref="IAppSettingsModel.SettingsReset"/>
        public event EventHandler? SettingsReset;

        /// <inheritdoc cref="IAppSettingsModel.SettingsSaved"/>
        public event EventHandler? SettingsSaved;

        /// <inheritdoc cref="IAppSettingsModel.SettingsLoaded"/>
        public event EventHandler? SettingsLoaded;
    }
}
