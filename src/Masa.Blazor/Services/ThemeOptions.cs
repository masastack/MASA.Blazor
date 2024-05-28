namespace BlazorComponent;

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

    public string? Surface { get; set; }

    public string? OnSurface { get; set; }

    public string? InverseSurface { get; set; }

    public string? InverseOnSurface { get; set; }

    public string? InversePrimary { get; set; }

    public Dictionary<string, string> UserDefined { get; } = new();
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