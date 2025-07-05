namespace Masa.Blazor;

public class ThemeOptions
{
    public bool IsDarkScheme { get; internal set; }

    public string ColorScheme => IsDarkScheme ? "dark" : "normal";

    public string? CombinePrefix { get; set; }

    public string? Primary { get; set; }

    public string? OnPrimary { get; set; }

    public string? Secondary { get; set; }

    public string? OnSecondary { get; set; }

    /// <summary>
    /// Teritary
    /// </summary>
    public string? Accent { get; set; }

    public string? OnAccent { get; set; }

    public string? Error { get; set; }

    public string? OnError { get; set; }

    public string? Info { get; set; }

    public string? OnInfo { get; set; }

    public string? Success { get; set; }

    public string? OnSuccess { get; set; }

    public string? Warning { get; set; }

    public string? OnWarning { get; set; }

    public string? SurfaceDim { get; set; }

    public string? Surface { get; set; }

    public string? SurfaceBright { get; set; }

    public string? SurfaceContainerLowest { get; set; }

    public string? SurfaceContainerLow { get; set; }

    public string? SurfaceContainer { get; set; }

    public string? SurfaceContainerHigh { get; set; }

    public string? SurfaceContainerHighest { get; set; }

    public string? OnSurface { get; set; }

    public string? InverseSurface { get; set; }

    public string? InverseOnSurface { get; set; }

    public string? InversePrimary { get; set; }

    // TODO: Scrim, Shadow

    public ThemeVariables Variables { get; } = MasaBlazorPreset.GetThemeVariables();

    public Dictionary<string, ColorPairing> UserDefined { get; } = new();

    public ThemeOptions ShallowCopy()
    {
        return (ThemeOptions)this.MemberwiseClone();
    }

    public string Build(string name)
    {
        return $$"""
                 .theme--{{name}} {
                   {{this.ToString()}}
                 } 
                 """;
    }

    public override string ToString()
    {
        return $"""
                color-scheme: {ColorScheme};
                {BuildCssVariable("primary", Primary, OnPrimary)}
                {BuildCssVariable("secondary", Secondary, OnSecondary)}
                {BuildCssVariable("accent", Accent, OnAccent)}
                {BuildCssVariable("info", Info, OnInfo)}
                {BuildCssVariable("success", Success, OnSuccess)}
                {BuildCssVariable("warning", Warning, OnWarning)}
                {BuildCssVariable("error", Error, OnError)}
                {BuildCssVariable(UserDefined)}

                {BuildCssVariable("surface-dim", SurfaceDim)}
                {BuildCssVariable("surface", Surface)}
                {BuildCssVariable("surface-bright", SurfaceBright)}
                {BuildCssVariable("surface-container-lowest", SurfaceContainerLowest)}
                {BuildCssVariable("surface-container-low", SurfaceContainerLow)}
                {BuildCssVariable("surface-container", SurfaceContainer)}
                {BuildCssVariable("surface-container-high", SurfaceContainerHigh)}
                {BuildCssVariable("surface-container-highest", SurfaceContainerHighest)}
                {BuildCssVariable("on-surface", OnSurface)}
                {BuildCssVariable("inverse-surface", InverseSurface)}
                {BuildCssVariable("inverse-on-surface", InverseOnSurface)}
                {BuildCssVariable("inverse-primary", InversePrimary)}

                {Variables}
 
                  --m-theme-outline: var(--m-theme-on-surface), var(--m-low-emphasis-opacity);
                  --m-theme-outline-variant: var(--m-theme-on-surface), var(--m-border-opacity);
                """;
    }
    
    public static string BuildCssVariable(string role, string? color, string? onColor)
    {
        var stringBuilder = new StringBuilder();

        if (!string.IsNullOrWhiteSpace(color))
        {
            var value = ColorParser.ParseColorAsString(color);
            stringBuilder.AppendLine(
                $"""
                   --m-theme-{role}: {value};
                   --m-theme-{role}-text: {value};
                 """);
        }

        if (!string.IsNullOrWhiteSpace(onColor))
        {
            var value = ColorParser.ParseColorAsString(onColor);
            stringBuilder.Append($"  --m-theme-on-{role}: {value};");
        }

        return stringBuilder.ToString();
    }

    private static string BuildCssVariable(Dictionary<string, ColorPairing> userDefined)
    {
        return string.Join("", userDefined.Select(x => BuildCssVariable(x.Key, x.Value.Color, x.Value.OnColor)));
    }

    private static string? BuildCssVariable(string name, string? color)
    {
        if (string.IsNullOrWhiteSpace(color))
        {
            return null;
        }

        return $"  --m-theme-{name}: {ColorParser.ParseColorAsString(color)};";
    }
}