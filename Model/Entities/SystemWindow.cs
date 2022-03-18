using System.Runtime.InteropServices;
using Common.Interfaces;
using Common.WinApi;

namespace Model.Entities
{
    /// <summary>
    /// Implements methods for working with system windows
    /// </summary>
    internal class SystemWindow
    {
        public nint Handler { get; }

        /// <summary>
        /// Checks if the application window is displayed
        /// </summary>
        /// <returns> true when window can be displayed </returns>
        private bool IsWindowValid() 
            => Handler != 0 && Native.IsWindowVisible(Handler);

        /// <summary>
        /// Checks if the window works in full screen, given a task bar
        /// </summary>
        /// <returns> <see cref="bool"/> true, when window expanded on full screen</returns>
        public bool IsWindowOnFullScreen(IScreenContext screen)
        {
            Native.GetWindowRect(new HandleRef(null, Handler), out var rect);
            return screen.Bounds.Width == rect.Right + rect.Left && screen.Bounds.Height == rect.Bottom + rect.Top;
        }

        /// <summary>
        /// Checks foreground Window bounds
        /// </summary>
        /// <returns> <see cref="bool"/> true, if the window is maximized to full screen </returns>
        //public static nint GetFullScreenForegroundWindow(ScreenEntity screen)
        //{
        //    var handle = Native.GetForegroundWindow();
        //    if (!IsWindowValid(handle) || IsWindowOnFullScreen(screen, handle)) return 0;
        //    return handle;
        //}

        public SystemWindow(nint handler)
        {
            Handler = handler;
        }
    }
}
