namespace Common.Entities;

/// <summary>
/// Разрешение экрана
/// </summary>
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