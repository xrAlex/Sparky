using System.Runtime.InteropServices;

namespace Common.WinApi.Entities;

[StructLayout(LayoutKind.Sequential)]
public struct GammaRamp
{
    [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
    public ushort[] Red { get; init; }

    [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
    public ushort[] Green { get; init; }

    [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
    public ushort[] Blue { get; init; }
}