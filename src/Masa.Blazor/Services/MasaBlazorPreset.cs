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
        OnPrimary = "#FFFFFF",
        OnSecondary = "#FFFFFF",
        OnAccent = "#FFFFFF",
        OnError = "#FFFFFF",
        OnInfo = "#FFFFFF",
        OnSuccess = "#FFFFFF",
        OnWarning = "#FFFFFF",
        OnSurface = "#000000DE",
        OnSurfaceVariant = "#000000DE",
        SurfaceDim = "#FFFFFF",
        Surface = "#FFFFFF",
        SurfaceBright = "#FFFFFF",
        SurfaceContainerLowest = "#FFFFFF",
        SurfaceContainerLow = "#FFFFFF",
        SurfaceContainer = "#f5f5f5",
        SurfaceContainerHigh = "#f5f5f5",
        SurfaceContainerHighest = "#f5f5f5",
        Outline = "#0000001f",
        OutlineVariant = "#0000001f",
        InverseSurface = "#121212",
        InverseOnSurface = "#FFFFFF",
        InversePrimary = "#2196F3"
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
        OnPrimary = "#000000",
        OnSecondary = "#000000",
        OnAccent = "#000000",
        OnError = "#FFFFFF",
        OnInfo = "#FFFFFF",
        OnSuccess = "#FFFFFF",
        OnWarning = "#FFFFFF",
        OnSurface = "#FFFFFF",
        OnSurfaceVariant = "#FFFFFF",
        SurfaceDim = "#121212",
        Surface = "#121212",
        SurfaceBright = "#121212",
        SurfaceContainerLowest = "#1E1E1E",
        SurfaceContainerLow = "#272727",
        SurfaceContainer = "#272727",
        SurfaceContainerHigh = "#272727",
        SurfaceContainerHighest = "#272727",
        Outline = "#FFFFFF1f",
        OutlineVariant = "#FFFFFF1f",
        InverseSurface = "#FFFFFF",
        InverseOnSurface = "#000000DE",
        InversePrimary = "#1976D2"
    };
}
