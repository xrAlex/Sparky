using Common.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces;

namespace Common.WinApi
{
    public static partial class WinApiWrapper
    {
        /// <summary>
        /// Создает констекст данных устройства отображения.
        /// </summary>
        /// <param name="screenSystemName"> Сисемное имя утрйоства отображения.</param>
        /// <returns> Дескриптор устройства отображения </returns>
        public static nint CreateScreenDeviceContext(string screenSystemName)
            => CreateDC(screenSystemName, string.Empty, string.Empty, 0);

        /// <summary>
        /// Удаляет контекст устрйоства отображения.
        /// </summary>
        /// <param name="deviceContext">Дескриптор контекста устройства.</param>
        public static bool DeleteScreenDeviceContext(nint deviceContext)
            => DeleteDC(deviceContext);

        /// <summary>
        /// Устанавливает цветовую гамму устрйоства отображения
        /// </summary>
        /// <param name="deviceContext">Дескриптор устройства отображения</param>
        /// <param name="gammaRamp">Буфер, содержащий устанавливаемую кривую гамма-коррекции</param>
        /// <returns>Результат выполнения операции</returns>
        public static bool SetScreenGamma(nint deviceContext, ref GammaRamp gammaRamp)
            => SetDeviceGammaRamp(deviceContext, ref gammaRamp);

        /// <summary>
        /// Возвращает путь до исполняемного файла процесса указанного окна
        /// </summary>
        /// <param name="windowHandle">Дескриптор окна</param>
        /// <returns><see cref="string"/> путь до исполняемого файла процесса окна</returns>
        public static string? TryGetExecutablePath(nint windowHandle)
        {
            GetWindowThreadProcessId(windowHandle, out var pId);
            string? processPath = null;

            if (pId != 0)
            {
                var openedHandler = OpenProcess(ProcessAccessFlags.QueryLimitedInformation, false, pId);

                if (openedHandler != 0)
                {
                    var path = TryGetProcessPath(openedHandler);

                    if (!string.IsNullOrWhiteSpace(path))
                    {
                        processPath = path;
                    }
                }
                CloseHandle(openedHandler);
            }

            return processPath;
        }


        /// <summary>
        /// Возвращает все дескрипторы видимых окон в системе.
        /// </summary>
        public static List<nint> GetAllVisibleWindows()
        {
            var windows = new List<nint>();
            var callback = new EnumWindowsProc((hWnd, _) =>
            {
                if (hWnd != 0 && IsWindowVisible(hWnd))
                {
                    windows.Add(hWnd);
                }
                return true;
            });

            EnumWindows(callback, 0);
            return windows;
        }

        /// <summary>
        /// Проверяет есть ли на указанном источнике отображения полноэкранное окно переднего плана.
        /// </summary>
        /// <returns> <see langword="true"/>, если найдено окно развернутое во весь экран.</returns>
        public static bool IsForegroundWindowOnFullScreen(ScreenBounds screenBounds, out nint windowHandle)
        {
            windowHandle = GetForegroundWindow();
            return IsWindowValid(windowHandle) && IsWindowOnFullScreen(windowHandle, screenBounds);
        }


        /// <summary>
        /// Проверяет развернуто ли указанное окно на весь экран, включая панель задач
        /// </summary>
        /// <returns> <see langword="true"/>, если окно отображается во весь экран</returns>
        public static bool IsWindowOnFullScreen(nint handle, ScreenBounds screenBounds)
        {
            GetWindowRect(new HandleRef(null, handle), out var rect);
            return screenBounds.Width == rect.Right + rect.Left && screenBounds.Height == rect.Bottom + rect.Top;
        }

        /// <summary>
        /// Проверяет если ли у процесса видимое окно
        /// </summary>
        /// <returns> <see langword="true"/>, если у процесса имеется видимое окно </returns>
        private static bool IsWindowValid(nint processHandle)
            => processHandle != 0 && IsWindowVisible(processHandle);

        /// <summary>
        /// Возвращает полный путь до исполняемого файла процесса.
        /// </summary>
        /// <param name="openedHandler"> Дескриптор процесса.</param>
        /// <returns> Если удалось найти путь, то возвращает <see cref="string"/> путь до исполняемого файла процесса. </returns>
        private static string? TryGetProcessPath(nint openedHandler)
        {
            var buffer = new StringBuilder(1024);
            var bufferSize = (uint)buffer.Capacity + 1;

            return QueryFullProcessImageName(openedHandler, 0, buffer, ref bufferSize) ? buffer.ToString() : null;
        }
    }
}
