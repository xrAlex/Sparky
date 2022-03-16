using System;
using System.IO;
using System.Text;
using Common.WinApi;

namespace Model.Entities
{
    internal sealed class SystemProcess
    {
        public nint WindowHandler { get; }

        /// <summary>
        /// Получает полный путь до исполняемого файла процесса
        /// </summary>
        /// <returns> Если удалось найти путь, то возвращает <see cref="string"/> путь до исполняемого файла процесса </returns>
        private static string? TryGetProcessPath(nint openedHandler)
        {
            var buffer = new StringBuilder(1024);
            var bufferSize = (uint)buffer.Capacity + 1;

            return Native.QueryFullProcessImageName(openedHandler, 0, buffer, ref bufferSize) ? buffer.ToString() : null;
        }

        public string? TryGetExecutablePath()
        {
            var pId = GetIdByWindow();
            string? processPath = null;

            if (pId != 0)
            {
                var openedHandler = Native.OpenProcess(ProcessAccessFlags.QueryLimitedInformation, false, pId);

                if (openedHandler != IntPtr.Zero)
                {
                    var path = TryGetProcessPath(openedHandler);

                    if (!string.IsNullOrWhiteSpace(path))
                    {
                        processPath = path;
                    }
                }
                CloseProcess(openedHandler);
            }

            return processPath;
        }

        private static void CloseProcess(nint handler) 
            => Native.CloseHandle(handler);


        /// <summary>
        /// Gets process id by window handle
        /// </summary>
        /// <returns> Process <see cref="uint"/> id </returns>
        private uint GetIdByWindow()
        {
            Native.GetWindowThreadProcessId(WindowHandler, out var pId);
            return pId;
        }
        
        public SystemProcess(nint windowHandler)
        {
            WindowHandler = windowHandler;
        }
    }
}
