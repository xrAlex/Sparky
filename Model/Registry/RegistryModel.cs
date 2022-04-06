using System;
using Common.Extensions;
using Common.Interfaces;

namespace Model.Registry
{
    /// <summary>
    /// A model for working with register entries
    /// </summary>
    internal sealed class RegistryModel : IRegistryModel
    {
        private const string ICMPath = "HKLM\\Software\\Microsoft\\Windows NT\\CurrentVersion\\ICM";
        private const string RunPath = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Run";
        private const string GammaParamName = "GdiICMGammaRange";
        private const string SilentLaunchKey = "-silent";

        /// <inheritdoc cref="IRegistryModel.IsExtendedGammaRangeActive"/>
        public bool IsExtendedGammaRangeActive()
        {
            var value = RegistryEx.TryGetRegistryValue(ICMPath, GammaParamName);

            if (value != null)
            {
                return (int)value == 256;
            }

            return true;
        }

        /// <inheritdoc cref="IRegistryModel.SetDefaultGammaRangeKey"/>
        public void SetDefaultGammaRangeKey()
            => RegistryEx.SetRegistryValue(ICMPath, GammaParamName, 0);

        /// <inheritdoc cref="IRegistryModel.SetExtendedGammaRangeKey"/>
        public void SetExtendedGammaRangeKey()
            => RegistryEx.SetRegistryValue(ICMPath, GammaParamName, 256);

        /// <inheritdoc cref="IRegistryModel.IsAppStartupKeyFounded"/>
        public bool IsAppStartupKeyFounded()
        {
            var value = RegistryEx.TryGetRegistryValue(RunPath, AppDomain.CurrentDomain.FriendlyName);
            return value != null;
        }

        /// <inheritdoc cref="IRegistryModel.AddAppStartupKey"/>
        public void AddAppStartupKey()
        {
            var value = RegistryEx.TryGetRegistryValue(RunPath, AppDomain.CurrentDomain.FriendlyName);

            if (value == null)
            {
                RegistryEx.SetRegistryValue(
                    RunPath,
                    AppDomain.CurrentDomain.FriendlyName,
                    $"{Environment.ProcessPath} {SilentLaunchKey}");
            }
        }

        /// <inheritdoc cref="IRegistryModel.DeleteAppStartupKey"/>
        public void DeleteAppStartupKey() 
            => RegistryEx.DeleteRegistryValue(RunPath, AppDomain.CurrentDomain.FriendlyName);
    }
}
