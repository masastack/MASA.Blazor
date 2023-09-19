namespace Masa.Blazor
{
    /// <summary>
    /// Cascading this will cause additional render,we may just cascading rtl in the feature
    /// </summary>
    public class MasaBlazor
    {
        private bool _rtl;

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
            Breakpoint.OnChanged = e =>
            {
                BreakpointChanged?.Invoke(this, e);

                if (e.MobileChanged)
                {
                    MobileChanged?.Invoke(this, e);
                }
            };

            Application = application;
            Theme = theme;
            Icons = icons;
            SSR = ssr;
            Defaults = defaults;
        }

        public bool RTL
        {
            get => _rtl;
            set
            {
                if (_rtl != value)
                {
                    _rtl = value;
                    OnRTLChange?.Invoke(_rtl);
                    RTLChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public Application Application { get; }

        public Breakpoint Breakpoint { get; }

        public IDictionary<string, IDictionary<string, object?>?>? Defaults { get; }

        public Theme Theme { get; }

        public Icons Icons { get; }

        public SSROptions? SSR { get; }

        [Obsolete("Use RTLChanged instead")]
        public event Action<bool>? OnRTLChange;

        public event EventHandler RTLChanged;

        public event Action<Theme>? OnThemeChange;

        /// <summary>
        /// An event that fires when the breakpoint has changed.
        /// </summary>
        public event EventHandler<BreakpointChangedEventArgs>? BreakpointChanged;

        /// <summary>
        /// An event that fires when the the value of Mobile property from <see cref="Breakpoint"/> has changed.
        /// </summary>
        public event EventHandler<MobileChangedEventArgs> MobileChanged;

        public void ToggleTheme()
        {
            Theme.Dark = !Theme.Dark;

            OnThemeChange?.Invoke(Theme);
        }
    }
}
