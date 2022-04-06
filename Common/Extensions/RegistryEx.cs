using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using Microsoft.Win32;

namespace Common.Extensions;

public static class RegistryEx
{
    /// <summary>
    /// Получает значение из реестра.
    /// </summary>
    /// <param name="path">Путь значения.</param>
    /// <param name="entryName">Имя значения.</param>
    /// <returns> <see cref="object"/>, содержущий значение реестра или null.</returns>
    public static object? TryGetRegistryValue(string path, string entryName)
    {
        var (hiveName, relativePath) = DeconstructPath(path);

        using var registryKey = GetRegistryKeyFromHiveName(hiveName).OpenSubKey(relativePath, false);
        return registryKey?.GetValue(entryName);
    }

    /// <summary>
    /// Устанавливает значение параметра в реестре Windows.
    /// </summary>
    /// <param name="path">Путь в реестре до значения.</param>
    /// <param name="entryName">Индентификатор значения. </param>
    /// <param name="entryValue">Новое значение.</param>
    public static void SetRegistryValue(string path, string entryName, object entryValue)
    {
        try
        {
            var (hiveName, relativePath) = DeconstructPath(path);
            using var registryKey = GetRegistryKeyFromHiveName(hiveName).CreateSubKey(relativePath, true);

            registryKey.SetValue(entryName, entryValue);
        }
        catch (Exception ex) when (ex is SecurityException || ex is UnauthorizedAccessException)
        {
            RunRegistryCli(new[]
            {
                "add", path,
                "/v", entryName,
                "/d", entryValue.ToString()!,
                "/t", GetRegistryValueType(entryValue.GetType()),
                "/f"
            }, true);
        }
    }

    /// <summary>
    /// Удаляет значение параметра в реестре Windows.
    /// </summary>
    /// <param name="path">Путь в реестре до значения.</param>
    /// <param name="entryName">Индентификатор значения. </param>
    public static void DeleteRegistryValue(string path, string entryName)
    {
        try
        {
            var (hiveName, relativePath) = DeconstructPath(path);
            using var registryKey = GetRegistryKeyFromHiveName(hiveName).OpenSubKey(relativePath, true);

            registryKey?.DeleteValue(entryName, false);
        }
        catch (Exception ex) when (ex is SecurityException || ex is UnauthorizedAccessException)
        {
            RunRegistryCli(new[]
            {
                "delete", path,
                "/v", entryName,
                "/f"
            }, true);
        }
    }

    /// <summary>
    /// Разбивает путь реестра для использования в <see cref="Registry"/>
    /// </summary>
    private static (string hiveName, string relativePath) DeconstructPath(string entryName)
    {
        var separatorIndex = entryName.IndexOf('\\');
        return (entryName[..separatorIndex], entryName[(separatorIndex + 1)..]);
    }

    /// <summary>
    /// Получает ключ <see cref="Registry"/> из строки пути реестра.
    /// </summary>
    private static RegistryKey GetRegistryKeyFromHiveName(string hiveName)
    {
        if (string.Equals(hiveName, "HKLM", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(hiveName, "HKEY_LOCAL_MACHINE", StringComparison.OrdinalIgnoreCase))
        {
            return Registry.LocalMachine;
        }

        if (string.Equals(hiveName, "HKCU", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(hiveName, "HKEY_CURRENT_USER", StringComparison.OrdinalIgnoreCase))
        {
            return Registry.CurrentUser;
        }

        throw new NotSupportedException($"Unsupported or invalid hive '{hiveName}'.");
    }

    /// <summary>
    /// Получает тип значения параметра реестра.
    /// </summary>
    private static string GetRegistryValueType(Type type)
    {
        if (type == typeof(int))
            return "REG_DWORD";

        if (type == typeof(string))
            return "REG_SZ";

        throw new NotSupportedException($"Unsupported registry value type '{type}'.");
    }

    /// <summary>
    /// Запускает системный компонент REG.exe с указанными параметрами.
    /// </summary>
    private static void RunRegistryCli(IEnumerable<string> arguments, bool asAdmin = false)
    {
        var processInfo = new ProcessStartInfo("reg");

        foreach (var arg in arguments)
        {
            processInfo.ArgumentList.Add(arg);
        }

        if (asAdmin)
        {
            processInfo.UseShellExecute = true;
            processInfo.Verb = "runas";
        }

        using var process = Process.Start(processInfo);
        process?.WaitForExit(3000);
    }
}