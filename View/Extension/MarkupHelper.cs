using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace View.Extension;

internal static class MarkupHelper
{
    private static bool ShowWindowCanExecute(Type winType)
        => typeof(Window).IsAssignableFrom(winType)
           && winType.GetConstructor(Type.EmptyTypes) != null;

    private static void ShowWindowExecute(Type winType, Window? owner)
    {
        var constructor = winType.GetConstructor(Type.EmptyTypes);
        if (constructor != null)
        {
            var window = (Window)constructor.Invoke(null);
            if (owner != null)
            {
                window.Owner = owner;
            }
            window.Show();
        }
    }

    /// <summary>
    /// Обработчик закрытия окна
    /// </summary>
    public static RoutedEventHandler CloseWindow { get; } = (sender, _) =>
    {
        var window = TryGetAncestor<Window>(sender as DependencyObject);
        window?.Close();
    };

    /// <summary>
    /// Обработчик события открытия окна
    /// </summary>
    public static RoutedEventHandler ShowWindow { get; } = (sender, _) =>
    {
        if (sender is ICommandSource { CommandParameter: Type commandParam })
        {
            if (ShowWindowCanExecute(commandParam))
            {
                var owner = TryGetAncestor<Window>(sender as DependencyObject);
                ShowWindowExecute(commandParam, owner);
            }
        }
    };

    /// <summary>
    /// Обработчик события открытия ссылки
    /// </summary>
    public static RoutedEventHandler OpenLink { get; } = (sender, _) =>
    {
        if (sender is ICommandSource { CommandParameter: string commandParam })
        {
            OpenLinkExecute(commandParam);
        }
    };

    public static void OpenLinkExecute(string link)
    {
        var pi = new ProcessStartInfo()
        {
            UseShellExecute = true,
            FileName = link
        };
        Process.Start(pi);
    }

    /// <summary>
    /// Возвращает текующию версию сборки приложения
    /// </summary>
    public static string GetAppVersion()
    {
        var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString();
        return version != null? $"Ver {version}" : "";
    }

    /// <summary>
    /// Обработчик события закрытия приложения
    /// </summary>
    public static RoutedEventHandler AppShutdown { get; } = (_, _)
        => Application.Current.Shutdown();


    /// <summary>
    /// Обработчик события сворачивания приложения в трей
    /// </summary>
    public static RoutedEventHandler ToTray { get; } = (_, _) =>
    {
        foreach (var wnd in Application.Current.Windows)
        {
            var window = wnd as Window;
            window?.Close();
        }

        App.TaskBarIcon!.Visibility = Visibility.Visible;
    };


    /// <summary>Поиск родителя в визуальном дереве</summary>
    /// <typeparam name="T">Тип родителя.</typeparam>
    /// <param name="element">Элемент с которого начинается поиск.</param>
    /// <returns>В поиск включается и сам элемент. Если он указанного типа,
    /// то будет он же и возвращён.</returns>
    private static T? TryGetAncestor<T>(DependencyObject? element) where T : DependencyObject
    {
        T? ancestor = null;
        while (element != null && (ancestor = element as T) == null)
        {
            element = VisualTreeHelper.GetParent(element);
        }
        return ancestor;
    }
}