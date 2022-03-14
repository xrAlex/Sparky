using System.Windows;
using System.Windows.Input;

namespace View.Views
{
    public partial class SettingsWindowView : Window
    {
        public SettingsWindowView() => InitializeComponent();

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();
    }
}