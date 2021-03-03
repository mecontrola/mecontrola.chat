using MeControla.Core.Extensions.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MeControla.Core.Tools.HtmlParse
{
    public class HTMLReader : IHTMLReader
    {
        private const string MATCH_GROUP_NAME = "name";
        private const string MATCH_GROUP_VALUE = "value";

        public HTMLElement Find(string html, string tag)
            => Find(html, tag, null);

        public HTMLElement Find(string html, string tag, HTMLAttributes attributes)
            => FindAll(html, tag, attributes, false).FirstOrDefault();

        public IList<HTMLElement> FindAll(string html, string tag)
            => FindAll(html, tag, null);

        public IList<HTMLElement> FindAll(string html, string tag, HTMLAttributes attributes)
            => FindAll(html, tag, attributes, true);

        private IList<HTMLElement> FindAll(string html, string tag, HTMLAttributes attributes, bool allElements)
        {
            var elm = InternalFindAll(html, tag, attributes, 0);
            var list = new List<HTMLElement>();

            if (elm.Item1 == -1)
            {
                return list;
            }

            if (!allElements)
            {
                list.Add(elm.Item2);

                return list;
            }

            do
            {
                list.Add(elm.Item2);

                elm = InternalFindAll(html, tag, attributes, elm.Item1);
            } while (elm.Item1 != -1);

            return list;
        }

        private (int, HTMLElement) InternalFindAll(string html, string tag, HTMLAttributes attributes, int position)
        {
            var initPos = GetInitPosition(html, tag, attributes, position);

            if (initPos.Item1 == -1)
            {
                return (-1, null);
            }

            while (initPos.Item1 > -1 && initPos.Item2 == 0)
            {
                initPos = GetInitPosition(html, tag, attributes, initPos.Item1);
            }

            if (initPos.Item1 == -1)
            {
                return (-1, null);
            }

            var endPos = GetEndPosition(html, tag, initPos.Item2);

            return (endPos, new HTMLElement
            {
                TagName = tag,
                Attributes = initPos.Item3,
                Content = html.Substring(initPos.Item2, endPos - initPos.Item2)
            });
        }

        private (int, int, HTMLAttributes) GetInitPosition(string html, string tag, HTMLAttributes attributes, int initPosition)
        {
            var initTag = $"<{tag}";
            var initPos = html.IndexOf(initTag, initPosition);

            if (initPos == -1)
            {
                return (-1, 0, null);
            }

            var termPos = html.IndexOf(">", initPos) + 1;
            var tagFull = html.Substring(initPos, termPos - initPos);
            var tagAttributes = GetAttributes(tagFull);

            if (tagAttributes.HasAttributeInTag(attributes))
            {
                return (initPos, termPos, tagAttributes);
            }

            return (termPos, 0, null);
        }

        private int GetEndPosition(string html, string tag, int initPosition)
        {
            var initTag = $"<{tag}";
            var termTag = $"</{tag}";

            var endPosInit = html.IndexOf(termTag, initPosition);
            var countRepeat = 0;
            var containTag = initPosition;

            while (containTag < endPosInit)
            {
                containTag = html.IndexOf(initTag, initPosition);

                if (containTag == -1 || containTag > endPosInit)
                {
                    break;
                }

                initPosition += html.Substring(containTag, html.IndexOf(">", containTag) - containTag + 1).Length;
                countRepeat++;
            }

            for (int i = 0; i < countRepeat; i++)
            {
                endPosInit = html.IndexOf(termTag, endPosInit + termTag.Length);
            }

            return endPosInit;
        }

        private static string GetRegexAttribute()
            => $@"(?<{MATCH_GROUP_NAME}>(\w(-\w+)?)+)(\s*?)=(\s*?)""(?<{MATCH_GROUP_VALUE}>(\w|\.|,|_|\-|\/|\s|\?|\=|&|;)+)""";

        private static HTMLAttributes GetAttributes(string html)
        {
            var matchs = Regex.Matches(html, GetRegexAttribute(), RegexOptions.IgnoreCase);

            if (matchs.Count == 0)
            {
                return null;
            }

            var attrs = new HTMLAttributes();

            foreach (Match match in matchs)
            {
                attrs.Add(match.Groups[MATCH_GROUP_NAME].Value,
                          match.Groups[MATCH_GROUP_VALUE].Value);
            }

            return attrs;
        }
    }
}