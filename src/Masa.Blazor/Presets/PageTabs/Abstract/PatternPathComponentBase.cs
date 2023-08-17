namespace Masa.Blazor.Presets;

public class PatternPathComponentBase : BDomComponentBase
{
    [Inject]
    protected NavigationManager NavigationManager { get; set; } = null!;

    /// <summary>
    /// A list of regular expression patterns to match.
    /// When multiple path matched, would only be displayed in the same tab.
    /// Similar to the `_self` of the HTML a tag's target attribute.
    /// </summary>
    /// <example>
    /// "/users/[a-z][A-Z]+": the /users/alice and /users/tom would be displayed in the same tab.
    /// </example>
    [Parameter]
    public IEnumerable<string>? SelfPatterns { get; set; }

    protected IEnumerable<Regex> FormatSelfPatterns()
    {
        return SelfPatterns is null
            ? Enumerable.Empty<Regex>()
            : SelfPatterns.Select(p => new Regex(p, RegexOptions.IgnoreCase));
    }

    protected PatternPath GetCurrentPatternPath()
    {
        var absolutePath = NavigationManager.GetAbsolutePath();
        var selfPatternRegexes = FormatSelfPatterns();
        var selfPatternRegex = selfPatternRegexes.FirstOrDefault(r => r.IsMatch(absolutePath));
        return selfPatternRegex is null ? new PatternPath(absolutePath) : new PatternPath(selfPatternRegex.ToString(), absolutePath);
    }
}
