using Common.Infrastructure.INPC;
using Common.Interfaces;

namespace Model.Entities
{
    internal sealed class Application : INPCBase, IApplication
    {
        private bool _isIgnored;
        private bool _onFullScreen;
        private string _executableFilePath;

        public string Name { get; }

        public string ExecutableFilePath
        {
            get => _executableFilePath;
            set => Set(ref _executableFilePath!, value);
        }

        public bool OnFullScreen
        {
            get => _onFullScreen;
            set => Set(ref _onFullScreen, value);
        }

        public bool IsIgnored
        {
            get => _isIgnored;
            set => Set(ref _isIgnored, value);
        }

        public override string ToString()
            => Name;

        public Application(string name, string exeFilePath)
        {
            Name = name;
            _executableFilePath = exeFilePath;
        }
    }
}