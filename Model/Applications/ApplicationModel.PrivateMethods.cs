using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Enums;
using Common.Extensions.CollectionChanged;
using Common.Interfaces;
using Common.WinApi;
using Model.Entities;

namespace Model.Applications
{
    internal partial class ApplicationModel
    {
        private void CollectionChanged(object? sender, ApplicationCollectionChangedArgs args)
        {
            switch (args.Action)
            {
                case CollectionChangedAction.Added:
                    break;
                case CollectionChangedAction.Removed:
                    break;
                case CollectionChangedAction.Updated:
                    if (args.PropertyName == "isIgnored")
                    {
                        if (args.NewValue != null)
                        {
                            var val = (bool)args.NewValue;
                            if (val)
                            {
                                _appSettings.IgnoredApplications.Add(args.App.ExecutableFilePath);
                            }
                            else
                            {
                                _appSettings.IgnoredApplications.Remove(args.App.ExecutableFilePath);
                            }
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            InternalCollectionChanged?.Invoke(this, args);
        }


        /// <summary>
        /// Получает все обработчики видимых окон в системе
        /// и создает на их основе <see cref="SystemWindow"/>
        /// </summary>
        /// <returns>Коллекцию окон <see cref="SystemWindow"/> </returns>
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

        /// <summary>
        /// Собирает информацию о всех окнах в системе и добавляет их в коллекцию.
        /// </summary>
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
                    _applications.Add(new Application(procFileName, executablePath)
                    {
                        OnFullScreen = IsApplicationWindowOnFullScreen(window),
                        IsIgnored = IsApplicationIgnored(executablePath)
                    });
                }
            }
        }

        private bool IsApplicationWindowOnFullScreen(SystemWindow window) 
            => _screenModel.GetAllScreens().Any(window.IsWindowOnFullScreen);

        private bool IsApplicationIgnored(string executablePath)
            => _appSettings.IgnoredApplications.Contains(executablePath);
    }
}
