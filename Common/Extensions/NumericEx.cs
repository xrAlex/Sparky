using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class NumericEx
    {
        public static float Lerp(this float startValue, float targetValue, float multiplier) 
            => targetValue * (1f - multiplier) + startValue * multiplier;

        public static bool IsCloseTo(this float value, float secondValue)
            => value > secondValue
                ? Math.Abs((secondValue - value) / value * 100) < 1
                : Math.Abs((value - secondValue) / secondValue * 100) < 1;

        public static T ClampMin<T>(this T value, T min) where T : IComparable<T> 
            => value.CompareTo(min) <= 0 ? min : value;

        public static T ClampMax<T>(this T value, T max) where T : IComparable<T> 
            => value.CompareTo(max) >= 0 ? max : value;

    }
}
