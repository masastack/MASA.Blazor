using System;

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
    }
}