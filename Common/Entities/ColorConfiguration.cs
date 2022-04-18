using System;

namespace Common.Entities;

public readonly struct ColorConfiguration: IEquatable<ColorConfiguration>
{
    public float Brightness { get; } = 1.0f;
    public float ColorTemperature { get; } = 6600f;

    public bool Equals(ColorConfiguration other) =>
        Math.Abs(Brightness - other.Brightness) < 0.1
        && Math.Abs(ColorTemperature - other.ColorTemperature) < 1;

    public ColorConfiguration(float colorTemperature, float brightness)
    {
        Brightness = brightness;
        ColorTemperature = colorTemperature;
    }
}