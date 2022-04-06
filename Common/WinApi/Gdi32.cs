using System.Runtime.InteropServices;
using Common.WinApi.Entities;

namespace Common.WinApi
{
    public static partial class WinApiWrapper
    {
        [DllImport("gdi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern nint CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, nint lpInitData);

        [DllImport("gdi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool DeleteDC(nint hDc);

        [DllImport("gdi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool SetDeviceGammaRamp(nint hDc, ref GammaRamp lpRamp);

        [DllImport("gdi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool GetDeviceGammaRamp(nint hDc, out GammaRamp lpRamp);
    }
}