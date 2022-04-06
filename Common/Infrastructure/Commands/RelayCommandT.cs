namespace Common.Infrastructure.Commands;

/// <summary>Реализация RelayCommand для методов с обобщённым параметром.</summary>
/// <typeparam name="T">Тип параметра методов.</typeparam>
public class RelayCommand<T> : RelayCommand
{
    /// <summary> Конструктор команды. </summary>
    /// <param name = "execute"> Выполняемый метод команды. </param>
    /// <param name = "canExecute"> Метод, возвращающий состояние команды. </param>
    /// <param name="converter">Опциональный конвертер <see cref="object"/> в <typeparamref name="T"/>. <br/>
    /// Вызывается если параметр
    /// <see href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/is">
    /// не совместим</see> с типом <typeparamref name="T"/>.
    /// </param>
    public RelayCommand(ExecuteHandler<T> execute, CanExecuteHandler<T>? canExecute, ConverterFromObjectHandler<T>? converter = null) : base
    (
        p =>
        {
            if (p is T t || converter != null && converter(p, out t))
            {
                execute(t);
            }
        },
        p => (p is T t || converter != null && converter(p, out t)) && (canExecute?.Invoke(t) ?? true)
    ){}

    /// <summary> Command constructor. </summary>
    /// <param name = "execute"> Выполняемый метод команды. </param>
    /// <param name="converter">Опциональный конвертер <see cref="object"/> в <typeparamref name="T"/>.</param>
    public RelayCommand(ExecuteHandler<T> execute, ConverterFromObjectHandler<T>? converter = null) : this(execute, null, converter) { }
}