namespace Masa.Blazor.Presets;

public record PageTabPathValue(string Path, bool Selected)
{
    public bool IsMatch(string pattern)
    {
        if (!pattern.StartsWith("/"))
        {
            pattern = "/" + pattern;
        }

        return new Regex(pattern, RegexOptions.IgnoreCase).IsMatch(Path);
    }
}
