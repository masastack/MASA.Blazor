using System.Linq;
using System.Text.RegularExpressions;

namespace Masa.Blazor.Doc.Models.Extensions;

public static class StringExtensions
{
    public static string HashSection(this string title)
    {
        title = title.ToLower();

        if (new[] { "api", "caveats" }.Contains(title))
            return title;

        return "section-" + HashHelper.Hash(title);
    }

    public static string RemoveTag(this string markup, string tag)
    {
        if (markup == null) return null;

        var start = $"<{tag}>";
        var end = $"</{tag}>";

        if (!markup.Contains(start) || !markup.Contains(end)) return markup;

        var from = markup.IndexOf(start);
        var to = markup.IndexOf(end) + end.Length;

        var tagContent = markup[from..to];

        return markup.Replace(tagContent, "");
    }

    public static string RemoveWrapTag(this string markup)
    {
        var regex = new Regex("<[a-z]+\\s?.*?>");
        var match = regex.Match(markup);
        if (!match.Success) return markup;

        var tag = match.Value.Split(" ")[0];
        tag = tag.Replace("<", "");
        tag = tag.Replace(">", "");

        var endTag = $"</{tag}>";

        markup = markup.Trim();
        if (!(markup.StartsWith(match.Value) && markup.EndsWith(endTag))) return markup;

        var from = match.Value.Length;
        var to = markup.IndexOf(endTag);

        return markup.Substring(from, to - from);
    }

    public static string StructureUrl(this string str)
    {
        return str.Replace(" ", "-").ToLower();
    }
}