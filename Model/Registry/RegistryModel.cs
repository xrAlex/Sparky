using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
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

        public bool IsExtendedGammaRangeActive()
        {
            var value = RegistryEx.TryGetRegistryValue(ICMPath, GammaParamName);

            if (value != null)
            {
                return (int)value == 256;
            }

            return true;
        }

        public void SetDefaultGammaRangeKey()
            => RegistryEx.SetRegistryValue(ICMPath, GammaParamName, 0);

        public void SetExtendedGammaRangeKey()
            => RegistryEx.SetRegistryValue(ICMPath, GammaParamName, 256);

        public bool IsAppStartupKeyFounded()
        {
            var value = RegistryEx.TryGetRegistryValue(RunPath, AppDomain.CurrentDomain.FriendlyName);
            return value != null;
        }

        public void AddAppStartupKey()
        {
            var value = RegistryEx.TryGetRegistryValue(RunPath, AppDomain.CurrentDomain.FriendlyName);

            if (value == null)
            {
                var path = Path.GetDirectoryName(Environment.ProcessPath);
                var pat2h = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);



                RegistryEx.SetRegistryValue(
                    RunPath,
                    AppDomain.CurrentDomain.FriendlyName,
                    $"{Environment.ProcessPath} {SilentLaunchKey}");
            }
        }

        public void DeleteAppStartupKey() 
            => RegistryEx.DeleteRegistryValue(RunPath, AppDomain.CurrentDomain.FriendlyName);
    }
}
