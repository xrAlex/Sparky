using System;
using System.Windows;
using System.Windows.Markup;
using View.Localization;

namespace View.Extension;

public class KeyToLocalizationResourceExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return KeyToLocalizationResourceConverter.Instance;
    }
}

/// <summary>Возвращает <see cref="MarkupHelper.CloseWindow"/>.</summary>
[MarkupExtensionReturnType(typeof(RoutedEventHandler))]
internal sealed class CloseWindowExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider) 
        => MarkupHelper.CloseWindow;
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