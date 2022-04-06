using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace View.Extension
{
    [ContentProperty(nameof(DataTemplates))]
    internal class TemplateSelector : DataTemplateSelector
    {
        // ReSharper disable once CollectionNeverUpdated.Global
        public Dictionary<object, DataTemplate> DataTemplates { get; set; } = new();

        public static DataTemplate Default { get; } = (DataTemplate)XamlReader.Parse(@"
        <DataTemplate
            xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
            <TextBlock Text=""{Binding StringFormat='Нет шаблона для этого значения: {0}'}""
                       TextWrapping=""Wrap""
                       Foreground=""Red""/>
        </DataTemplate>
        ");

        public override DataTemplate SelectTemplate(object? item, DependencyObject container)
        {
            if (item != null)
            {
                if (DataTemplates.TryGetValue(item, out var template))
                    return template;
            }
            return Default;
        }
    }
}
