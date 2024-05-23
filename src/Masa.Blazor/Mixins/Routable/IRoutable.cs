using System.Text.RegularExpressions;

namespace Masa.Blazor;

public interface IRoutable
{
    IDictionary<string, object?> Attributes { get; }

    bool Disabled { get; }

    string? Href { get; }

    bool Link { get; }

    EventCallback<MouseEventArgs> OnClick { get; }

    string? Tag { get; }

    string? Target { get; }

    /// <summary>
    /// Determines whether the current location should match the exact <see cref="Href"/> path.
    /// Do not use with <see cref="MatchPattern"/>.
    /// </summary>
    bool Exact { get; }

    /// <summary>
    /// The regular expression pattern to match the current location. The active class will be applied if matched.
    /// </summary>
    /// <example>
    /// "/foo/(bar|baz)/qux" will match /foo/bar/qux and /foo/baz/qux.
    /// </example>
    string? MatchPattern { get; }

    NavigationManager NavigationManager { get; }

    public bool IsClickable => !Disabled && (IsLink || OnClick.HasDelegate || Tabindex > 0);

    public bool IsLink => Href != null || Link;

    public int Tabindex => Attributes.TryGetValue("tabindex", out var tabindex) ? Convert.ToInt32(tabindex) : 0;

    public(string tag, Dictionary<string, object?>) GenerateRouteLink()
    {
        string tag;
        Dictionary<string, object?> attrs = new(Attributes);

        if (Href != null)
        {
            tag = "a";
            attrs["href"] = Href;
        }
        else
        {
            tag = Tag ?? "div";
        }

        if (Target != null)
        {
            attrs["target"] = Target;
        }

        return (tag, attrs);
    }

    public bool MatchRoute()
    {
        if (Href is null) return false;

        var absolutePath = NavigationManager.GetAbsolutePath();

        return MatchRoute(Href, absolutePath, Exact, MatchPattern);
    }

    public static bool MatchRoute(string href, string absolutePath, bool exact, string? matchPattern)
    {
        href = FormatUrl(href);

        if (!string.IsNullOrWhiteSpace(matchPattern))
        {
            return Regex.Match(absolutePath, matchPattern, RegexOptions.IgnoreCase).Success;
        }

        if (exact || href == "/")
        {
            href += "/?$";
        }

        href = "^" + href;

        return Regex.Match(absolutePath, href, RegexOptions.IgnoreCase).Success;
    }

    private static string FormatUrl(string url)
    {
        if (!url.StartsWith("/"))
        {
            return "/" + url;
        }

        return url;
    }
}
