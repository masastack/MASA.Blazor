using BlazorComponent;
using System.Text.RegularExpressions;

namespace Masa.Blazor.Doc.Utils
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

        public static List<(AlertTypes type, string markup)> SplitByAlertType(this string markup)
        {
            if (markup == null) return null;

            markup = markup.Trim();

            var regex = new Regex("<!--alert:\\S+-->");
            var regexEnd = new Regex("<!--/alert:\\S+-->");

            List<(AlertTypes, string)> list = new();

            var matches = regex.Matches(markup).Where(m => m.Success).Select(m => m.Value).ToArray();
            var matchEnds = regexEnd.Matches(markup).Where(m => m.Success).Select(m => m.Value).ToArray();

            var lastMatchEndIndex = 0;
            for (int i = 0; i < matches.Length; i++)
            {
                var match = matches[i];
                var from = markup.IndexOf(match);
                var type = GetAlertType(match);

                if (matchEnds.Length - 1 < i)
                {
                    if (lastMatchEndIndex + 1 < from) // exists string between <!--alert--> and <!--/alert-->
                    {
                        var str = markup.Substring(lastMatchEndIndex, from - lastMatchEndIndex);
                        if (!string.IsNullOrWhiteSpace(str))
                        {
                            list.Add((AlertTypes.None, str));
                        }
                    }

                    list.Add((type, markup.Substring(from)));
                    break;
                }

                var matchEnd = matchEnds[i];

                if (i == 0 && from > 0) // exists string before the first matched alert
                {
                    var str = markup[..from];
                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        list.Add((AlertTypes.None, str));
                    }
                }
                else if (lastMatchEndIndex + 1 < from) // exists string between <!--alert--> and <!--/alert-->
                {
                    var str = markup.Substring(lastMatchEndIndex, from - lastMatchEndIndex);
                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        list.Add((AlertTypes.None, str));
                    }
                }

                var to = markup.IndexOf(matchEnd) + matchEnd.Length;

                lastMatchEndIndex = to;

                var p = markup.Substring(from, to - from);

                list.Add((type, p));

                if (matchEnds.Length - 1 == i && to < markup.Length) // exists string after the last matched end alert
                {
                    var str = markup[to..];
                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        list.Add((AlertTypes.None, str));
                    }
                }

                markup = regex.Replace(markup, "", 1);
                markup = regexEnd.Replace(markup, "", 1);

                lastMatchEndIndex -= (match.Length + matchEnd.Length);
            }

            if (!list.Any())
                list.Add((AlertTypes.None, markup));

            return list;
        }

        public static List<(string lang, string markup)> SplitByCode(this string markup)
        {
            if (markup == null) return null;

            markup = markup.Trim();

            var start = $"<pre>";
            var end = $"</pre>";

            var regex = new Regex(start);
            var regexEnd = new Regex(end);

            List<(string, string)> list = new();

            var matches = regex.Matches(markup).Where(m => m.Success).Select(m => m.Value).ToArray();
            var matchEnds = regexEnd.Matches(markup).Where(m => m.Success).Select(m => m.Value).ToArray();

            var lastMatchEndIndex = 0;
            for (int i = 0; i < matches.Length; i++)
            {
                var match = matches[i];
                var from = markup.IndexOf(match);
                string lang = null;

                if (matchEnds.Length - 1 < i)
                {
                    if (lastMatchEndIndex + 1 < from) // exists string between <!--alert--> and <!--/alert-->
                    {
                        var str = markup.Substring(lastMatchEndIndex, from - lastMatchEndIndex);
                        if (!string.IsNullOrWhiteSpace(str))
                        {
                            list.Add((null, str));
                        }
                    }

                    var res = markup.Substring(from);
                    lang = GetCodeLanguage(res);
                    list.Add((lang, res));
                    break;
                }

                var matchEnd = matchEnds[i];

                if (i == 0 && from > 0) // exists string before the first matched alert
                {
                    var str = markup[..from];
                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        list.Add((null, str));
                    }
                }
                else if (lastMatchEndIndex + 1 < from) // exists string between <!--alert--> and <!--/alert-->
                {
                    var str = markup.Substring(lastMatchEndIndex, from - lastMatchEndIndex);
                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        list.Add((null, str));
                    }
                }

                var to = markup.IndexOf(matchEnd) + matchEnd.Length;

                lastMatchEndIndex = to;

                var p = markup.Substring(from, to - from);
                lang = GetCodeLanguage(p);

                list.Add((lang, p));

                if (matchEnds.Length - 1 == i && to < markup.Length) // exists string after the last matched end alert
                {
                    var str = markup[to..];
                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        list.Add((null, str));
                    }
                }

                markup = regex.Replace(markup, "", 1);
                markup = regexEnd.Replace(markup, "", 1);

                lastMatchEndIndex -= (match.Length + matchEnd.Length);
            }

            if (!list.Any())
                list.Add((null, markup));

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

        private static string GetCodeLanguage(string markup)
        {
            var regex = new Regex("language-\\S+(?=\")");

            var match = regex.Match(markup);

            if (!match.Success) return null;

            var value = match.Value["language-".Length..];

            return value switch
            {
                "c#" => "csharp",
                _ => value
            };
        }
    }
}