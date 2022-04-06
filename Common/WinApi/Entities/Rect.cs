using System.Runtime.InteropServices;

namespace Common.WinApi.Entities
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Rect
    {
        public int Left { get; }

        public int Top { get; }

        public int Right { get; }

        public int Bottom { get; }
    }
}
