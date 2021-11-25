using System.Text.RegularExpressions;
using BlazorComponent;

namespace MASA.Blazor.Doc.Utils
{
    public static class StringExtensions
    {
        public static string TrimWhiteSpace(this string str)
        {
            if (str == null)
            {
                return null;
            }

            char[] whiteSpace = { '\r', '\n', '\f', '\t', '\v' };
            return str.Trim(whiteSpace).Trim();
        }

        public static string FixLineBreakForWeb(this string str)
        {
            return str.Replace(Environment.NewLine, "<br/>");
        }

        public static string FixTabsForWeb(this string str)
        {
            return str.Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");
        }

        public static string FixSpaceForWeb(this string str)
        {
            return str.Replace(" ", "&nbsp;");
        }

        public static string FormatLink(this string markup)
        {
            if (markup == null) return null;

            return ApisHelper.FormatMarkup(markup);
        }

        public static List<(AlertTypes, string)> SplitByAlertType(this string markup)
        {
            if (markup == null) return null;

            markup = markup.Trim();

            var regex = new Regex("<!--alert:\\S+-->");

            List<(AlertTypes, string)> list = new();

            var matches = regex.Matches(markup).Where(m => m.Success).Select(m => m.Value).ToArray();

            for (int i = 0; i < matches.Length; i++)
            {
                var match = matches[i];

                var from = markup.IndexOf(match);

                if (i == 0 && from > 0) // exists string before the first matched alert
                {
                    list.Add((AlertTypes.None, markup[..from]));
                }

                int to;
                if (i + 1 < matches.Length)
                {
                    var nextMatch = matches[i + 1];
                    to = markup.IndexOf(nextMatch);
                }
                else
                {
                    to = markup.Length - 1;
                }

                var type = GetAlertType(markup);
                var p = markup.Substring(from, to - from);

                markup = regex.Replace(markup, "", 1);

                list.Add((type, p));
            }

            if (!list.Any())
                list.Add((AlertTypes.None, markup));

            return list;
        }

        private static AlertTypes GetAlertType(string markup)
        {
            var from = markup.IndexOf(":") + 1;
            var to = markup.IndexOf("-->");
            var value = markup.Substring(from, to - from);

            return value.ToLower() switch
            {
                "error" => AlertTypes.Error,
                "info" => AlertTypes.Info,
                "success" => AlertTypes.Success,
                "warning" => AlertTypes.Warning,
                _ => AlertTypes.None
            };
        }
    }
}