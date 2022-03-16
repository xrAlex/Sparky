namespace Model.Entities
{
    internal sealed class Application
    {
        public string Name { get; }
        public string? ExecutableFilePath { get; set; }
        public bool OnFullScreen { get; set; }

        public override string ToString()
            => $"{Name} {(OnFullScreen ? "[FullScreen]" : "")}";

        public Application(string name)
        {
            Name = name;
        }
    }
}