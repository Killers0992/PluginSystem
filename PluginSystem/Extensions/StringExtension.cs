using System;

namespace PluginSystem.Extensions
{
    public static class StringExtension
    {
        public static Version ToVersion(this string str)
        {
            if (Version.TryParse(str, out Version ver))
                return ver;

            return new Version(1, 0, 0);
        }
    }
}
