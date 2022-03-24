using System.Runtime.InteropServices;
using System.Text;
using Common.Entities;

namespace Common.WinApi
{
    public static partial class WinApiWrapper
    {
        public delegate bool EnumWindowsProc(nint hWnd, nint lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(HandleRef hWnd, out Rect lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern nint GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(nint hWnd, out uint pId);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(nint hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(nint hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, nint lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool IsWindowVisible(nint hWnd);
    }
}
