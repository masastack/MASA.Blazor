namespace Masa.Blazor.Presets;

public class PatternPathComponentBase : MasaComponentBase
{
    [Inject] protected NavigationManager NavigationManager { get; set; } = null!;

    /// <summary>
    /// A list of regular expression patterns to match.
    /// When multiple path matched, would only be displayed in the same tab.
    /// Similar to the `_self` of the HTML a tag's target attribute.
    /// </summary>
    /// <example>
    /// "/users/[a-z][A-Z]+": the /users/alice and /users/tom would be displayed in the same tab.
    /// </example>
    [Parameter]
    public IEnumerable<string> SelfPatterns { get; set; } = Array.Empty<string>();

    private HashSet<string> _prevSelfPatterns = new();

    protected HashSet<Regex> CachedSelfPatternRegexes = new();

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (_prevSelfPatterns.SetEquals(SelfPatterns)) return;

        _prevSelfPatterns = new HashSet<string>(SelfPatterns);
        CachedSelfPatternRegexes =
            new HashSet<Regex>(SelfPatterns.Select(p => new Regex(p, RegexOptions.IgnoreCase)));
    }

    protected virtual PatternPath GetCurrentPatternPath()
    {
        var absolutePath = NavigationManager.GetAbsolutePath();
        var selfPatternRegex = CachedSelfPatternRegexes.FirstOrDefault(r => r.IsMatch(absolutePath));
        return selfPatternRegex is null
            ? new PatternPath(absolutePath)
            : new PatternPath(selfPatternRegex.ToString(), absolutePath);
    }
}