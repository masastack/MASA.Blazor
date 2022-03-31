using BlazorComponent;
using System.Text.RegularExpressions;

namespace Masa.Blazor.Doc.Utils;

public static class ApisHelper
{
    public static string FormatMarkup(string markup)
    {
        const string child = "<i class=\"m-icon theme--light primary--text {0}\" style=\"font-size: .875rem\"></i>";

        var attrs = "class=\"app-link text-decoration-none primary--text font-weight-medium d-inline-block\" ";
        var regex = new Regex("<a[^>]+href=\"(.*?)\"[^>]*>(.*?)</a>");

        List<(string old, string @new)> list = new();
        regex.Matches(markup).ForEach(m =>
        {
            var v = m.Value;

            var href = GetApiHref(m.Value);
            var (target, css) = GetTargetAndIconCss(href);

            if (target != null && !m.Value.Contains("target"))
            {
                attrs += " target=\"_blank\"";
            }

            v = v.Replace("<a ", $"<a {attrs}");
            v = v.Replace("</a>", $"{string.Format(child, css)}</a>");

            list.Add((m.Value, v));
        });

        list.ForEach(item => markup = markup.Replace(item.old, item.@new));

        return markup;
    }

    private static string GetApiHref(string markup)
    {
        var href = "href=\"";
        var from = markup.IndexOf(href) + href.Length;
        var to = markup.LastIndexOf("\">");

        return markup.Substring(from, to - from);
    }

    private static (string target, string css) GetTargetAndIconCss(string href)
    {
        var isExternal = href.StartsWith("http") || href.StartsWith("mailto");
        var isSamePage = !isExternal && href.StartsWith("#");

        var css = isSamePage ? "mr-1" : "ml-1";

        if (isExternal) return ("_blank", $"mdi mdi-open-in-new {css}");
        if (isSamePage) return (null, $"mdi mdi-pound {css}");
        return (null, $"mdi mdi-page-next {css}");
    }
}