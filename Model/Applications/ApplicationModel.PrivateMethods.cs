﻿using System;
using System.IO;
using System.Linq;
using Common.Enums;
using Common.Extensions.CollectionChanged;
using Common.WinApi;
using Model.Entities.Domain;

namespace Model.Applications;

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
                throw new ArgumentOutOfRangeException($"Application collection unknown action {args.Action}");
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

    /// <summary>
    /// Проверяет работает ли приложение в полноэкранном режиме на любом из источников отображения
    /// </summary>
    /// <param name="handle">Дескриптор окна</param>
    private bool IsApplicationWindowOnFullScreen(nint handle) 
        => _screenModel
            .GetAllScreens()
            .Select(screen =>
            {
                var screenBounds = screen.Bounds;
                return WinApiWrapper.IsWindowOnFullScreen(handle, ref screenBounds);
            })
            .FirstOrDefault();

    /// <summary>
    /// Проверяет не находится ли путь приложения в игнорируемых пользователем
    /// </summary>
    /// <param name="executablePath">Путь по исполняемого файла приложения</param>
    private bool IsApplicationIgnored(string executablePath)
        => _appSettings.IgnoredAppRepository.Contains(executablePath);
}