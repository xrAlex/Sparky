using Common.Infrastructure.Commands;
using System;
using System.Windows;
using System.Windows.Controls;

namespace View.Views
{
    public partial class SettingsNavigator : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            throw new System.NotImplementedException();
        }
    }
    public partial class SettingsNavigator
    {

        /// <summary>
        /// Текущее представление.
        /// </summary>
        public UserControl Current
        {
            get { return (UserControl)GetValue(CurrentProperty); }
            set { SetValue(CurrentProperty, value); }
        }

        /// <summary><see cref="DependencyProperty"/> для свойства <see cref="Current"/>.</summary>
        public static readonly DependencyProperty CurrentProperty =
            DependencyProperty.Register(nameof(Current), typeof(UserControl), typeof(SettingsNavigator), new PropertyMetadata(null));

        public RelayCommand SetCurrent { get; }

        public SettingsNavigator()
        {
            SetCurrent = new RelayCommand(SetCurrentExecute, SetCurrentCanExecute);
        }

        private bool SetCurrentCanExecute(object parameter)
        {
            return parameter == null || parameter is UserControl;
        }

        private void SetCurrentExecute(object parameter)
        {
            Current = (UserControl) parameter;
        }
    }
}