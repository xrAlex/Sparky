using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public readonly struct ColorConfiguration
    {
        public float Brightness { get; }
        public float ColorTemperature { get; }

        public ColorConfiguration(float colorTemperature, float brightness)
        {
            Brightness = brightness;
            ColorTemperature = colorTemperature;
        }
    }
}
