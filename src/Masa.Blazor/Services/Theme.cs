namespace Masa.Blazor;

public class Theme
{
    [Obsolete]
    public Theme(bool dark, ThemeOptions lightTheme, ThemeOptions darkTheme)
    {
        Dark = dark;
        Themes = new Themes(lightTheme, darkTheme);

        if (dark)
        {
            DefaultTheme = "dark";
        }
    }

    public Theme(ThemeOptions lightTheme, ThemeOptions darkTheme)
    {
        Themes = new Themes(lightTheme, darkTheme);
        DefaultTheme = "light";
    }

    [Obsolete($"Use {nameof(DefaultTheme)} instead.")]
    public bool Dark { get; set; }

    /// <summary>
    /// Specifies the default theme to be used, which can either be "light", "dark" or a custom theme name.
    /// </summary>
    public string DefaultTheme { get; set; } = "light";

    public Themes Themes { get; }

    public ThemeOptions CurrentTheme => Themes[DefaultTheme];
}