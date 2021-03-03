using MeControla.Core.Tools.HtmlParse;
using System.Text.RegularExpressions;

namespace MeControla.Core.Extensions.Tools
{
    public static class HTMLParameterExtension
    {
        public static bool HasAttributeInTag(this HTMLAttributes actual, HTMLAttributes expected)
        {
            if (expected == null)
                return true;

            if (actual == null)
                return false;

            if (!actual.ExistsAllKeys(expected))
                return false;

            return actual.ExistsAllValues(expected);
        }

        public static string RemoveTagAndContent(this string html)
        {
            var regex = @"<(\w|\d)+(\s|.)*</(\w|\d)+>";

            return Regex.Replace(html, regex, string.Empty).Trim();
        }

        public static string HtmlRemoveEncondeDecode(this string str)
            => str.Replace("&nbsp;", " ").Trim();
    }
}