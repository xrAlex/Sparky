using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class StringEx
    {
        /// <summary>
        /// Возвращает часть строки до разделителя.
        /// </summary>
        /// <remarks>Если в части строки до разделителя нет сиволов, то изнчальную строку.</remarks>
        public static string SubstringUntil(this string str, string sub, StringComparison comparison = StringComparison.Ordinal)
        {
            int length = str.IndexOf(sub, comparison);
            return length < 0 ? str : str.Substring(0, length);
        }

        /// <summary>
        ///     Возвращает часть строки после разделителя
        /// </summary>
        ///<remarks>Если после разделителя нет сиволов, то возвращает пустую строку.</remarks>
        public static string SubstringAfter(this string str, string sub, StringComparison comparison = StringComparison.Ordinal)
        {
            int num = str.IndexOf(sub, comparison);
            return num < 0 ? string.Empty : str.Substring(num + sub.Length, str.Length - num - sub.Length);
        }
    }
}
