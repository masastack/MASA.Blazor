namespace Masa.Blazor
{
    public class MasaBlazorOptions
    {
        /// <summary>
        /// Default is false
        /// </summary>
        public bool DarkTheme { get; set; }

        /// <summary>
        /// The theme options
        /// </summary>
        public ThemeOptions Theme { get; set; } = new ThemeOptions()
        {
            CombinePrefix = ".m-application",
            Primary = "#1867C0",
            Secondary = "#5CBBF6",
            Accent = "#005CAF",
            Error = "#FF5252",
            Info = "#2196F3",
            Success = "#4CAF50",
            Warning = "#FB8C00",
            UserDefined = new Dictionary<string, string>() { { "Tertiary", "#e57373" } },
        };

        public bool RTL { get; set; }

        public Breakpoint Breakpoint { get; } = new()
        {
            MobileBreakpoint = Constants.Breakpoint.MobileBreakpoint,
            ScrollBarWidth = Constants.Breakpoint.ScrollBarWidth,
            Thresholds = new BreakpointThresholds()
            {
                Sm = Constants.Breakpoint.Thresholds.Sm,
                Xs = Constants.Breakpoint.Thresholds.Xs,
                Md = Constants.Breakpoint.Thresholds.Md,
                Lg = Constants.Breakpoint.Thresholds.Lg,
            }
        };

        public void UseTheme(Action<ThemeOptions> themeAction)
        {
            themeAction.Invoke(Theme);
        }
    }
}
