namespace Masa.Blazor;

public class ThemeOptions
{
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
    
    public string? SurfaceVariant { get; set; }

    public string? OnSurfaceVariant { get; set; }

    public string? Outline { get; set; }

    public string? OutlineVariant { get; set; }

    public string? InverseSurface { get; set; }

    public string? InverseOnSurface { get; set; }

    public string? InversePrimary { get; set; }

    // TODO: Scrim, Shadow

    public Variables Variables { get; } = new();

    public Dictionary<string, ColorPairing> UserDefined { get; } = new();
}

public record ColorPairing(string Color, string? OnColor = null);

public class Variables
{
    public float IdleOpacity { get; set; } = 0.04f;
    public float HoverOpacity { get; set; } = 0.04f;
    public float FocusOpacity { get; set; } = 0.12f;
    public float DisabledOpacity { get; set; } = 0.38f;
    public float SelectedOpacity { get; set; } = 0.08f;
    public float ActivatedOpacity { get; set; } = 0.12f;
    public float HighlightOpacity { get; set; } = 0.32f;
    public float HighEmphasisOpacity { get; set; } = 0.87f;
    public float MediumEmphasisOpacity { get; set; } = 0.6f;
    public float LowEmphasisOpacity { get; set; } = 0.38f;

    public override string ToString()
    {
        return $"""
                --m-idle-opacity: {IdleOpacity};
                --m-hover-opacity: {HoverOpacity};
                --m-focus-opacity: {FocusOpacity};
                --m-disabled-opacity: {DisabledOpacity};
                --m-selected-opacity: {SelectedOpacity};
                --m-activated-opacity: {ActivatedOpacity};
                --m-highlight-opacity: {HighlightOpacity};
                --m-high-emphasis-opacity: {HighEmphasisOpacity};
                --m-medium-emphasis-opacity: {MediumEmphasisOpacity};
                --m-low-emphasis-opacity: {LowEmphasisOpacity};
                """;
    }
}

public class Theme
{
    public Theme(bool dark, ThemeOptions lightTheme, ThemeOptions darkTheme)
    {
        Dark = dark;
        Themes = new Themes(lightTheme, darkTheme);
    }

    public bool Dark { get; set; }

    public Themes Themes { get; }

    public ThemeOptions CurrentTheme => Dark ? Themes.Dark : Themes.Light;
}

public class Themes
{
    public Themes(ThemeOptions light, ThemeOptions dark)
    {
        Light = light;
        Dark = dark;
    }

    public ThemeOptions Dark { get; }

    public ThemeOptions Light { get; }
}