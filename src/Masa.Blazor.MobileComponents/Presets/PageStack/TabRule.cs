using System.Text.RegularExpressions;

namespace Masa.Blazor.Presets.PageStack;

/// <summary>
/// Rule for <see cref="PPageStack"/> tabs.
/// </summary>
/// <param name="Pattern">The regular expression pattern to match.</param>
/// <param name="Persistent">>Whether the tab should be persistent. If not, the tab would not be cached in the DOM.</param>
/// <param name="Self">Similar to the `_self` of the HTML a tag's target attribute.</param>
public record TabRule(string Pattern, bool Persistent = true, bool Self = false)
{
    public Regex Regex { get; } = new(Pattern, RegexOptions.IgnoreCase);

    public override string ToString() => Pattern;
}