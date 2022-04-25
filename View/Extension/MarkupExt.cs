using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace View.Extension;

/// <summary>
/// Возвращает <see cref="MarkupHelper.CloseWindow"/>.
/// </summary>
[MarkupExtensionReturnType(typeof(RoutedEventHandler))]
internal sealed class CloseWindowExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider) 
        => MarkupHelper.CloseWindow;
}


/// <summary>
/// Возвращает <see cref="string"/>.
/// </summary>
[MarkupExtensionReturnType(typeof(string))]
internal sealed class GetAppVersionExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
        => MarkupHelper.GetAppVersion();
}

/// <summary>
/// Возвращает <see cref="MarkupHelper.ShowWindow"/>.
/// </summary>
[MarkupExtensionReturnType(typeof(RoutedEventHandler))]
internal sealed class ShowWindowExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider) 
        => MarkupHelper.ShowWindow;
}


/// <summary>
/// Возвращает <see cref="MarkupHelper.DragWindow"/>.
/// </summary>
[MarkupExtensionReturnType(typeof(MouseButtonEventHandler))]
internal sealed class DragWindowExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
        => MarkupHelper.DragWindow;
}

/// <summary>
/// Возвращает <see cref="MarkupHelper.OpenLink"/>.
/// </summary>
[MarkupExtensionReturnType(typeof(RoutedEventHandler))]
internal sealed class OpenLinkExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
        => MarkupHelper.OpenLink;
}

/// <summary>
/// Возвращает <see cref="MarkupHelper.AppShutdown"/>.
/// </summary>
[MarkupExtensionReturnType(typeof(RoutedEventHandler))]
internal sealed class AppShutdown : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
        => MarkupHelper.AppShutdown;
}

/// <summary>
/// Возвращает <see cref="MarkupHelper.ToTray"/>.
/// </summary>
[MarkupExtensionReturnType(typeof(RoutedEventHandler))]
internal sealed class ToTray : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
        => MarkupHelper.ToTray;
}