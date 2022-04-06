using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Common.Infrastructure.Commands
{
    /// <summary>Класс реализующий <see cref="ICommand"/>.<br/>
    /// Реализация взята из <see href="https://www.cyberforum.ru/wpf-silverlight/thread2390714-page4.html#post13535649"/>
    /// и дополнена конструктором для методов без параметра.</summary>
    public class RelayCommand : ICommand
    {
        private readonly CanExecuteHandler<object>? _canExecute;
        private readonly ExecuteHandler<object>? _execute;
        private readonly Action _invalidate;
        private static readonly Dispatcher Dispatcher = Application.Current.Dispatcher;

        /// <summary>Событие извещающее об изменении состояния команды.</summary>
        public event EventHandler? CanExecuteChanged;


        /// <summary>Конструктор команды.</summary>
        /// <param name="execute">Выполняемый метод команды.</param>
        /// <param name="canExecute">Метод, возвращающий состояние команды.</param>
        public RelayCommand(ExecuteHandler<object>? execute, CanExecuteHandler<object>? canExecute = null) : this()
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <inheritdoc cref="RelayCommand(ExecuteHandler, CanExecuteHandler)"/>
        public RelayCommand(ExecuteHandler execute, CanExecuteHandler? canExecute = null) : this
            (_ => execute(),
                _ => canExecute?.Invoke() ?? true
            )
        { }

        /// <summary>Метод, подымающий событие <see cref="CanExecuteChanged"/>.</summary>
        public void RaiseCanExecuteChanged()
        {
            if (Dispatcher.CheckAccess())
            {
                _invalidate();
            }
            else
            {
                _ = Dispatcher.BeginInvoke(_invalidate);
            }
        }
        private RelayCommand()
        {
            _invalidate = () => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

            void RequerySuggested(object? o, EventArgs e)
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }

            CommandManager.RequerySuggested += RequerySuggested;
        }

        /// <summary>Вызов метода, возвращающего состояние команды.</summary>
        /// <param name="parameter">Параметр команды.</param>
        /// <returns><see langword="true"/> - если выполнение команды разрешено.</returns>
        public bool CanExecute(object? parameter)
        {
            return parameter == null || (_canExecute?.Invoke(parameter) ?? true);
        }

        /// <summary>Вызов выполняющего метода команды.</summary>
        /// <param name="parameter">Параметр команды.</param>
        public void Execute(object? parameter)
        {
            if (parameter != null)
            {
                _execute?.Invoke(parameter);
            }
        }
    }
}
