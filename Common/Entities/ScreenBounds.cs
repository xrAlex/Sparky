using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public readonly struct ScreenBounds
    {
        public int Width { get; }
        public int Height { get; }

        public ScreenBounds(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
