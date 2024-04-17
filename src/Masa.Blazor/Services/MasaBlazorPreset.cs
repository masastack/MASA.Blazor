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
        OnPrimary = "#FFFFFF",
        Secondary = "#424242",
        OnSecondary = "#FFFFFF",
        Accent = "#82B1FF",
        OnAccent = "#FFFFFF",
        Error = "#FF5252",
        OnError = "#FFFFFF",
        Info = "#2196F3",
        OnInfo = "#FFFFFF",
        Success = "#4CAF50",
        OnSuccess = "#FFFFFF",
        Warning = "#FB8C00",
        OnWarning = "#FFFFFF",
        Surface = "#FFFFFF",
        OnSurface = "#000000DE",
        InverseSurface = "#121212",
        InverseOnSurface = "#FFFFFF",
        InversePrimary = "#2196F3"
    };

    private static ThemeOptions DarkTheme => new()
    {
        CombinePrefix = ".m-application",
        Primary = "#2196F3",
        OnPrimary = "#000000",
        Secondary = "#424242",
        OnSecondary = "#000000",
        Accent = "#FF4081",
        OnAccent = "#000000",
        Error = "#FF5252",
        OnError = "#FFFFFF",
        Info = "#2196F3",
        OnInfo = "#FFFFFF",
        Success = "#4CAF50",
        OnSuccess = "#FFFFFF",
        Warning = "#FB8C00",
        OnWarning = "#FFFFFF",
        Surface = "#121212",
        OnSurface = "#FFFFFF",
        InverseSurface = "#FFFFFF",
        InverseOnSurface = "#000000DE",
        InversePrimary = "#1976D2"
    };
}
