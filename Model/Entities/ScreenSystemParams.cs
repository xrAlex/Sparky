using Common.Entities;

namespace Model.Entities;

/// <summary>
/// Системные параметры устройства отображения
/// </summary>
internal sealed class ScreenSystemParams
{
    public ScreenBounds Bounds { get; }
    public string SystemName { get; }
    public string FriendlyName { get; }
    public int DisplayCode { get; }
    public nint Handle { get; }

    public ScreenSystemParams(int displayCode, string friendlyName,
        string systemName, ScreenBounds bounds, nint handle)
    {
        DisplayCode = displayCode;
        FriendlyName = friendlyName;
        SystemName = systemName;
        Bounds = bounds;
        Handle = handle;
    }
}