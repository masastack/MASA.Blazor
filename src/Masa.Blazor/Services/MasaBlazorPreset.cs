namespace Masa.Blazor;

public static class MasaBlazorPreset
{
    public static Breakpoint Breakpoint => new()
    {
        MobileBreakpoint = 1264,
        ScrollBarWidth = 16,
        Thresholds = new BreakpointThresholds()
        {
            Xs = 600,
            Sm = 960,
            Md = 1280,
            Lg = 1920
        }
    };

    public static Icons Icons => new(IconSet.MaterialDesignIcons, new MaterialDesignIconsAliases());

    public static Theme Theme => new(false, LightTheme, DarkTheme);

    private static ThemeOptions LightTheme => new()
    {
        CombinePrefix = ".m-application",
        Primary = "#1976D2",
        Secondary = "#424242",
        Accent = "#82B1FF",
        Error = "#FF5252",
        Info = "#2196F3",
        Success = "#4CAF50",
        Warning = "#FB8C00",
    };

    private static ThemeOptions DarkTheme => new()
    {
        CombinePrefix = ".m-application",
        Primary = "#2196F3",
        Secondary = "#424242",
        Accent = "#FF4081",
        Error = "#FF5252",
        Info = "#2196F3",
        Success = "#4CAF50",
        Warning = "#FB8C00",
    };
}
