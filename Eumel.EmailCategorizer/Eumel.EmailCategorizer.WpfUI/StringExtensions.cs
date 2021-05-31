using System;

namespace Eumel.EmailCategorizer.Outlook
{
    internal static class StringExtensions
    {
        /// <summary>
        ///     get string from source between topicStart and topicEnd
        /// </summary>
        public static string Between(this string source, string topicStart, string topicEnd)
        {
            if (source == null) return null;
            if (topicStart == null || topicEnd == null) return string.Empty;
            if (source.IsNullOrWhiteSpace()) return string.Empty;

            var start = source.IndexOf(topicStart, StringComparison.Ordinal);
            var end = source.IndexOf(topicEnd, start + 1, StringComparison.Ordinal);

            if (end == -1) // search from beginning again
                end = source.IndexOf(topicEnd, StringComparison.Ordinal);
            if (start > end) Switch(ref start, ref end);

            if (start < 0 || end < 0) return string.Empty; // because not found

            return source.Substring(start + 1, end - start - 1);
        }

        /// <summary>
        ///     switch two values
        /// </summary>
        private static void Switch(ref int a, ref int b)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }

        #region static string wrappers

        public static bool IsNullOrWhiteSpace(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        public static bool IsNull(this string source)
        {
            return source == null;
        }

        #endregion static string wrappers
    }
}