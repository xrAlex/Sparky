using System;

namespace Common.Entities;

public readonly struct ColorConfiguration
{
    public float Brightness { get; } = 1.0f;
    public float ColorTemperature { get; } = 6000f;

    public bool Equals(ref ColorConfiguration other)
    {
        return Math.Abs(Brightness - other.Brightness) < 0.1 
               && Math.Abs(ColorTemperature - other.ColorTemperature) < 1;
    }

    public ColorConfiguration(float colorTemperature, float brightness)
    {
        Brightness = brightness;
        ColorTemperature = colorTemperature;
    }
}