using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.DTO;
using Common.Extensions.CollectionChanged;
using Common.WinApi;
using Model.Entities;

namespace Model.Applications
{
    internal partial class ApplicationModel
    {
        private void CollectionChanged(object? sender, ApplicationCollectionChangedArgs e)
            => InternalCollectionChanged?.Invoke(this, e);

        private static List<SystemWindow> GetAllVisibleWindows()
        {
            var windows = new List<SystemWindow>();

            var callback = new Native.EnumWindowsProc((hWnd, _) =>
            {
                if (hWnd != 0 && Native.IsWindowVisible(hWnd))
                {
                    windows.Add(new SystemWindow(hWnd));
                }
                return true;
            });

            Native.EnumWindows(callback, 0);
            return windows;
        }

        private void FillApplicationsCollection()
        {
            var windows = GetAllVisibleWindows();

            foreach (var window in windows)
            {
                var process = new SystemProcess(window.Handler);
                var executablePath = process.TryGetExecutablePath();

                if (executablePath != null)
                {
                    var procFileName = Path.GetFileNameWithoutExtension(executablePath);
                    _applications.Add(new ApplicationDTO(procFileName)
                    {
                        ExecutableFilePath = executablePath,
                        OnFullScreen = false // TODO: надо поправить
                    });
                }
            }
        }

        //private bool IsFullScreenProcess(nint handle)
        //{
        //    return _settingsService.Screens
        //        .Select(screen => SystemWindow.IsWindowOnFullScreen(screen, handle))
        //        .FirstOrDefault();
        //}
    }
}
