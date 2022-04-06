namespace Common.Entities;

public readonly struct ColorConfiguration
{
    public float Brightness { get; } = 1.0f;
    public float ColorTemperature { get; } = 6000f;

    public ColorConfiguration(float colorTemperature, float brightness)
    {
        Brightness = brightness;
        ColorTemperature = colorTemperature;
    }
}