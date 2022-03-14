using System;
using System.Windows;
using System.Windows.Markup;

namespace View.Extension
{
    /// <summary>Возвращает <see cref="MarkupHelper.CloseWindow"/>.</summary>
    [MarkupExtensionReturnType(typeof(RoutedEventHandler))]
    public class CloseWindowExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) 
            => MarkupHelper.CloseWindow;
    }
    /// <summary>
    /// Возвращает <see cref="MarkupHelper.ShowWindow"/>.
    /// </summary>
    [MarkupExtensionReturnType(typeof(RoutedEventHandler))]
    public class ShowWindowExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) 
            => MarkupHelper.ShowWindowClicker;
    }
}
