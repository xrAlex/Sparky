using System;

namespace Common.Extensions;

public static class NumericEx
{
    /// <summary>
    /// Линейно интерполирует два значения между собой на основе множителя.
    /// </summary>
    /// <returns>Промежуточное значениие.</returns>
    public static float Lerp(this float startValue, float targetValue, float multiplier) 
        => targetValue * (1f - multiplier) + startValue * multiplier;

    /// <summary>
    /// Проверяет насколько близко первое значение ко второму
    /// </summary>
    /// <param name="value"></param>
    /// <param name="secondValue"></param>
    /// <returns>Если первое значение отличается от второго на 1%,<see langword="true"/></returns>
    public static bool IsCloseTo(this float value, float secondValue)
        => value > secondValue
            ? Math.Abs((secondValue - value) / value * 100) < 1
            : Math.Abs((value - secondValue) / secondValue * 100) < 1;

    /// <summary>
    /// Проверяет что переданное значение не меньше минимального.
    /// </summary>
    /// <param name="value">Значение.</param>
    /// <param name="min">Минимальное значение.</param>
    /// <returns>Если значение меньше минимального, то возвращает минимальное,
    /// иначе возвращает текущее значение</returns>
    public static T ClampMin<T>(this T value, T min) where T : IComparable<T> 
        => value.CompareTo(min) <= 0 ? min : value;

    /// <summary>
    /// Проверяет что переданное значение не больше максимального.
    /// </summary>
    /// <param name="value">Значение.</param>
    /// <param name="max">Максимальное значение.</param>
    /// <returns>Если значение больше максимального, то возвращает максимальное,
    /// иначе возвращает текущее значение</returns>
    public static T ClampMax<T>(this T value, T max) where T : IComparable<T> 
        => value.CompareTo(max) >= 0 ? max : value;

}