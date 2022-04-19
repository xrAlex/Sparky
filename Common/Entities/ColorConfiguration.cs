using System;

namespace Common.Entities;

public readonly struct ColorConfiguration: IEquatable<ColorConfiguration>
{
    public float Brightness { get; }
    public float ColorTemperature { get; }

    public ColorConfiguration(float colorTemperature, float brightness)
    {
        Brightness = brightness;
        ColorTemperature = colorTemperature;
    }


    /// <summary>
    /// Проверяет насколько текущие значения полей структуры близки к полям сравниваемой структуры
    /// </summary>
    /// <param name="target">Сравниваемая структура</param>
    /// <returns>Если зачения отличаются менее чем на на 10%, возвращает <see langword="true"/></returns>
    public bool IsCloseTo(ColorConfiguration target)
    {
        var br = Math.Abs(Brightness - target.Brightness);
        var cc = Math.Abs(ColorTemperature - target.ColorTemperature);

        return br < 0.1 && cc < 100;
    }
    
    public static bool operator == (ColorConfiguration cc1, ColorConfiguration cc2) 
        => cc1.Equals(cc2);

    public static bool operator != (ColorConfiguration cc1, ColorConfiguration cc2) 
        => !cc1.Equals(cc2);

    public override bool Equals(object? obj)
        => obj?.GetType() == typeof(ColorConfiguration)
           && Equals((ColorConfiguration)obj);

    public bool Equals(ColorConfiguration other)
        => Math.Abs(Brightness - other.Brightness) < 0.1
           && Math.Abs(ColorTemperature - other.ColorTemperature) < 1;

    public override int GetHashCode()
        => HashCode.Combine(Brightness, ColorTemperature);

    public override string ToString()
        => $"Color temperature: {ColorTemperature} Brightness: {Brightness} ";

}