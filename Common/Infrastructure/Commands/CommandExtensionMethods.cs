using System.Windows.Input;

namespace Common.Infrastructure.Commands;

public static class CommandExtensionMethods
{
    public static bool TryExecute(this ICommand command, object? parameter)
    {
        var can = command.CanExecute(parameter);

        if (can)
        {
            command.Execute(parameter);
        }

        return can;
    }
    public static bool TryExecute(this ICommand command) => TryExecute(command, null);
}