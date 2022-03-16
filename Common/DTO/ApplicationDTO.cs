using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class ApplicationDTO
    {
        public string Name { get; }
        public string? ExecutableFilePath { get; set; }
        public bool OnFullScreen { get; set; }

        public override string ToString()
            => Name;

        public ApplicationDTO(string name)
        {
            Name = name;
        }
    }
}
