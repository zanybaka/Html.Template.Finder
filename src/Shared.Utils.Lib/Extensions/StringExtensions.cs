using System;
using System.Collections.Generic;

namespace Shared.Utils.Lib.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<string> SplitByLength(this string text, int length)
        {
            for (int index = 0; index < text.Length; index += length)
            {
                yield return text.Substring(index, Math.Min(length, text.Length - index));
            }
        }

        public static string FirstNChars(this string text, int count, bool addEllipses)
        {
            if (text == null)
            {
                return null;
            }

            if (!addEllipses)
            {
                return text.Length > count
                    ? text.Substring(0, count)
                    : text;
            }

            return text.Length > count
                ? (count > 3 ? $"{text.Substring(0, count - 3)}..." : "...")
                : text;
        }

        public static string FirstWordsButNotMoreThanNChars(this string text, int maxChars)
        {
            if (text == null)
            {
                return null;
            }

            if (text.Length <= maxChars)
            {
                return text;
            }

            int pos = text.LastIndexOf(' ', maxChars);
            if (pos == -1)
            {
                return text.FirstNChars(maxChars, false);
            }

            return $"{text.Substring(0, pos)}";
        }

        public static string SkipFirstWord(this string text, char delimeter = ' ')
        {
            if (text == null)
            {
                return null;
            }

            int pos = text.IndexOf(delimeter);
            if (pos == -1)
            {
                return text;
            }

            return text.Substring(pos + 1);
        }
    }
}
