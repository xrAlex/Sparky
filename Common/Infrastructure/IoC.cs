using SimpleInjector;

namespace Common.Infrastructure;

public class IoC
{
    private static Container? _instance;
    public static Container Instance => _instance ??= new Container();

    public static T GetInstance<T>() where T : class
        => Instance.GetInstance<T>();

    private IoC(){}
}