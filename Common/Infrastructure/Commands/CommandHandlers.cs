namespace Common.Infrastructure.Commands
{
    public delegate void ExecuteHandler();
    public delegate bool CanExecuteHandler();

    public delegate void ExecuteHandler<in T>(T parameter);
    public delegate bool CanExecuteHandler<in T>(T parameter);

    public delegate bool ConverterFromObjectHandler<T>(in object value, out T result);
}
