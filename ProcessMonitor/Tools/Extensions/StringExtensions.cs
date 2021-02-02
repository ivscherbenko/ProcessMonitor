using System;

namespace ProcessMonitor.Tools.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsCaseInsensitive(this string source, string value)
        {
            return !string.IsNullOrEmpty(source) && source.IndexOf(value, StringComparison.InvariantCultureIgnoreCase) >= 0; 
        }
    }
}
