using Common.Infrastructure.INPC;
using Common.Interfaces;

namespace Model.Entities.Domain
{
    internal sealed class Application : INPCBase, IApplication
    {
        private bool _isIgnored;
        private bool _onFullScreen;
        private string _executableFilePath;

        /// <inheritdoc cref="IApplication.Name"/>
        public string Name { get; }

        /// <inheritdoc cref="IApplication.ExecutableFilePath"/>
        public string ExecutableFilePath
        {
            get => _executableFilePath;
            set => Set(ref _executableFilePath!, value);
        }

        /// <inheritdoc cref="IApplication.OnFullScreen"/>
        public bool OnFullScreen
        {
            get => _onFullScreen;
            set => Set(ref _onFullScreen, value);
        }

        /// <inheritdoc cref="IApplication.IsIgnored"/>
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