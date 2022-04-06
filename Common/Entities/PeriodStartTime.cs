namespace Common.Entities;

public readonly struct PeriodStartTime
{
    public byte Hour { get; }
    public byte Minute { get; }

    public PeriodStartTime(byte hour, byte minute)
    {
        Hour = hour;
        Minute = minute;
    }
}