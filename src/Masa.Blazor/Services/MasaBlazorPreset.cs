namespace Masa.Blazor;

internal static class MasaBlazorPreset
{
    internal static ThemeOptions LightTheme { get; } = new()
    {
        IsDarkScheme = false,
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
        OnSurface = "#000000",
        SurfaceDim = "#FFFFFF",
        Surface = "#FFFFFF",
        SurfaceBright = "#FFFFFF",
        SurfaceContainerLowest = "#FFFFFF",
        SurfaceContainerLow = "#FFFFFF",
        SurfaceContainer = "#FFFFFF",
        SurfaceContainerHigh = "#f5f5f5",
        SurfaceContainerHighest = "#e0e0e0",
        Outline = "#dcdcdc",
        OutlineVariant = "#dcdcdc",
        InverseSurface = "#121212",
        InverseOnSurface = "#FFFFFF",
        InversePrimary = "#2196F3",
        SurfaceVariant = "#424242", // 不是material设计规范的颜色角色
        OnSurfaceVariant = "#EEEEEE"
    };

    internal static ThemeOptions DarkTheme { get; } = new()
    {
        IsDarkScheme = true,
        CombinePrefix = ".m-application",
        Primary = "#a5c8ff",
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
        SurfaceDim = "#121212",
        Surface = "#121212",
        SurfaceBright = "#121212",
        SurfaceContainerLowest = "#212121", // 212121
        SurfaceContainerLow = "#212121",
        SurfaceContainer = "#212121", // 272727
        SurfaceContainerHigh = "#303030", //303030
        SurfaceContainerHighest = "#424242",
        Outline = "#393939",
        OutlineVariant = "#393939",
        InverseSurface = "#FFFFFF",
        InverseOnSurface = "#000000DE",
        InversePrimary = "#1976D2",
        SurfaceVariant = "#c8c8c8",
        OnSurfaceVariant = "#000000"
    };

    internal static Breakpoint Breakpoint { get; } = new()
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

    internal static Icons Icons { get; } = new(IconSet.MaterialDesignIcons, new MaterialDesignIconsAliases());

    internal static Theme Theme { get; } = new(LightTheme, DarkTheme);
}