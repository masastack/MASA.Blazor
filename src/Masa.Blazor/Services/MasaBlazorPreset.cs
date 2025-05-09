namespace Masa.Blazor;

internal static class MasaBlazorPreset
{
    internal static ThemeOptions GetLightTheme() => new()
    {
        IsDarkScheme = false,
        CombinePrefix = ".m-application",
        Primary = "#4318ff",
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
        InverseSurface = "#121212",
        InverseOnSurface = "#FFFFFF",
        InversePrimary = "#2196F3",
        SurfaceVariant = "#424242", // 不是material设计规范的颜色角色
        OnSurfaceVariant = "#EEEEEE"
    };

    internal static ThemeOptions GetDarkTheme() => new()
    {
        IsDarkScheme = true,
        CombinePrefix = ".m-application",
        Primary = "#C5C0FF",
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
        SurfaceContainerLowest = "#111111",
        SurfaceContainerLow = "#1E1E1E",
        SurfaceContainer = "#2B2B2B",
        SurfaceContainerHigh = "#383838",
        SurfaceContainerHighest = "#454545",
        InverseSurface = "#FFFFFF",
        InverseOnSurface = "#000000",
        InversePrimary = "#1976D2",
        SurfaceVariant = "#c8c8c8",
        OnSurfaceVariant = "#000000"
    };

    internal static Breakpoint GetBreakpoint() => new()
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

    internal static Icons GetIcons() => new(IconSet.MaterialDesignIcons, new MaterialDesignIconsAliases());

    internal static Theme GetTheme() => new(GetLightTheme(), GetDarkTheme());
    
    internal static ThemeVariables GetThemeVariables() => new()
    {
        BorderOpacity = 0.12f,
        IdleOpacity = 0.04f,
        HoverOpacity = 0.04f,
        FocusOpacity = 0.12f,
        DisabledOpacity = 0.38f,
        ActivatedOpacity = 0.12f,
        HighlightOpacity = 0.32f,
        HighEmphasisOpacity = 0.87f,
        MediumEmphasisOpacity = 0.6f,
        LowEmphasisOpacity = 0.38f
    };
}