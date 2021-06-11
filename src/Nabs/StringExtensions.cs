using System;

namespace Nabs
{
    public static class StringExtensions
    {
        public static string IsNullOrWhitespaceOrDefault(this string value, string defaultValue)
        {
            return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
        }
    }
}