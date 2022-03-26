using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Enums;
using Common.Extensions.CollectionChanged;
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
                    if (args.PropertyName == nameof(args.App.IsIgnored))
                    {
                        if (args.NewValue != null)
                        {
                            var val = (bool)args.NewValue;
                            if (val)
                            {
                                _appSettings.IgnoredAppRepository.Add(args.App.ExecutableFilePath);
                            }
                            else
                            {
                                _appSettings.IgnoredAppRepository.Delete(args.App.ExecutableFilePath);
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
        /// Собирает информацию о всех окнах в системе и добавляет их в коллекцию.
        /// </summary>
        private void FillApplicationsCollection()
        {
            var windows = WinApiWrapper.GetAllVisibleWindows();

            foreach (var handle in windows)
            {
                var executablePath = WinApiWrapper.TryGetExecutablePath(handle);

                if (executablePath != null)
                {
                    var procFileName = Path.GetFileNameWithoutExtension(executablePath);
                    _applications.Add(new Application(procFileName, executablePath)
                    {
                        OnFullScreen = IsApplicationWindowOnFullScreen(handle),
                        IsIgnored = IsApplicationIgnored(executablePath)
                    });
                }
            }
        }

        private bool IsApplicationWindowOnFullScreen(nint handle) 
            => _appSettings.ScreenRepository
                .GetData()
                .Values
                .Select(screen => WinApiWrapper.IsWindowOnFullScreen(handle, screen.Bounds))
                .FirstOrDefault();

        private bool IsApplicationIgnored(string executablePath)
            => _appSettings.IgnoredAppRepository.Contains(executablePath);
    }
}
