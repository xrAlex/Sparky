namespace Common.Interfaces
{
    public interface IApplication
    {
        string Name { get; }
        string ExecutableFilePath { get; set; }
        bool OnFullScreen { get; set; }
        bool IsIgnored { get; set; }
        string ToString();
    }
}
