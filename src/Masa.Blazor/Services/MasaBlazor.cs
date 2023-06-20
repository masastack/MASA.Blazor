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
            IDictionary<string, IDictionary<string, object?>?>? defaults = null)
        {
            RTL = rtl;
            Breakpoint = breakpoint;
            Breakpoint.OnChanged = e => { BreakpointChanged?.Invoke(this, e); };

            Application = application;
            Theme = theme;
            Icons = icons;
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

        [Obsolete("Use RTLChanged instead")]
        public event Action<bool>? OnRTLChange;

        public event Action<Theme>? OnThemeChange;

        public event EventHandler RTLChanged;

        public event EventHandler<BreakpointChangedEventArgs>? BreakpointChanged;

        public void ToggleTheme()
        {
            Theme.Dark = !Theme.Dark;

            OnThemeChange?.Invoke(Theme);
        }
    }
}
