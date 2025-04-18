namespace Masa.Blazor
{
    public class MasaBlazor
    {
        public MasaBlazor(
            bool rtl,
            Breakpoint breakpoint,
            Application application,
            Theme theme,
            Icons icons,
            SSROptions? ssr = null,
            IDictionary<string, IDictionary<string, object?>?>? defaults = null)
        {
            RTL = rtl;
            Breakpoint = breakpoint;
            Breakpoint.OnWindowResize = e =>
            {
                WindowSizeChanged?.Invoke(this, e);

                if (e.BreakpointChanged)
                {
                    BreakpointChanged?.Invoke(this, e);
                }

                if (e.MobileChanged)
                {
                    MobileChanged?.Invoke(this, e);
                }
            };
            Application = application;
            Theme = theme;
            Icons = icons;
            Ssr = ssr;
            Defaults = defaults;
        }

        public bool RTL
        {
            get;
            set
            {
                if (field != value)
                {
                    field = value;
                    OnRTLChange?.Invoke(field);
                    RTLChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public Application Application { get; }

        public Breakpoint Breakpoint { get; }

        public IDictionary<string, IDictionary<string, object?>?>? Defaults { get; private set; }

        public Theme Theme { get; }

        public Icons Icons { get; }

        public SSROptions? Ssr { get; }

        public bool IsSsr => Ssr is not null;

        [Obsolete("Use RTLChanged instead")]
        public event Action<bool>? OnRTLChange;

        public event EventHandler? RTLChanged;

        public event Action<Theme>? OnThemeChange;

        /// <summary>
        /// An event that fires when the window size has changed.
        /// </summary>
        public event EventHandler<WindowSizeChangedEventArgs>? WindowSizeChanged;

        /// <summary>
        /// An event that fires when the breakpoint has changed.
        /// </summary>
        public event EventHandler<BreakpointChangedEventArgs>? BreakpointChanged;

        /// <summary>
        /// An event that fires when the value of Mobile property from <see cref="Breakpoint"/> has changed.
        /// </summary>
        public event EventHandler<MobileChangedEventArgs> MobileChanged;

        public event EventHandler? DefaultsChanged;

        public void ToggleTheme()
        {
            Theme.Dark = !Theme.Dark;

            OnThemeChange?.Invoke(Theme);
        }

        public void SetTheme(bool dark)
        {
            if (Theme.Dark == dark)
            {
                return;
            }

            ToggleTheme();
        }
        
        // TODO: 动态更新主题
        public void UpdateTheme(Action<Theme> themeConfig)
        {
            themeConfig.Invoke(Theme);
            OnThemeChange?.Invoke(Theme);
        }

        /// <summary>
        /// Update the default configuration.
        /// </summary>
        /// <param name="defaultsConfig"></param>
        public void UpdateDefaults(Action<IDictionary<string, IDictionary<string, object?>?>> defaultsConfig)
        {
            Defaults ??= new Dictionary<string, IDictionary<string, object?>?>();
            defaultsConfig.Invoke(Defaults);
            DefaultsChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}