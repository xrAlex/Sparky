﻿using System.Windows;
using System.Windows.Controls;
using Common.Infrastructure.Commands;

namespace View.Extension;

internal sealed partial class PageNavigator : Freezable
{
    protected override Freezable CreateInstanceCore()
    {
        throw new System.NotImplementedException();
    }
}

/// <summary>
/// Навигатор для UserControl внутри окна.
/// </summary>
internal sealed partial class PageNavigator
{

    /// <summary>
    /// Текущее представление.
    /// </summary>
    public UserControl Current
    {
        get => (UserControl)GetValue(CurrentProperty);
        set => SetValue(CurrentProperty, value);
    }

    /// <summary><see cref="DependencyProperty"/> для свойства <see cref="Current"/>.</summary>
    public static readonly DependencyProperty CurrentProperty =
        DependencyProperty.Register(nameof(Current), typeof(UserControl), typeof(PageNavigator), new PropertyMetadata(null));

    public RelayCommand SetCurrent { get; }

    public PageNavigator()
    {
        SetCurrent = new RelayCommand(SetCurrentExecute, SetCurrentCanExecute);
    }

    private bool SetCurrentCanExecute(object? parameter)
    {
        return parameter == null || parameter is UserControl;
    }

    private void SetCurrentExecute(object? parameter)
    {
        if (parameter is UserControl uc)
        {
            Current = uc;
        }
    }
}