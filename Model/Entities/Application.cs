using Common.Interfaces;

namespace Model.Entities
{
    internal sealed class Application : IApplication
    {
        public string Name { get; }
        public string? ExecutableFilePath { get; set; }
        public bool OnFullScreen { get; set; }
        public bool IsIgnored { get; set; }

        public override string ToString()
            => Name;

        public Application(string name)
        {
            Name = name;
        }
    }
}