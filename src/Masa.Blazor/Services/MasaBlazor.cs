#nullable enable

namespace Masa.Blazor
{
    /// <summary>
    /// Cascading this will cause additional render,we may just cascading rtl in the feature
    /// </summary>
    public class MasaBlazor
    {
        private bool _rtl;

        public MasaBlazor(
            Breakpoint breakpoint,
            Application application,
            Theme theme,
            Icons icons,
            IDictionary<string, IDictionary<string, object?>?>? defaults = null)
        {
            Breakpoint = breakpoint;
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
                }
            }
        }

        public Application Application { get; }

        public Breakpoint Breakpoint { get; }

        public IDictionary<string, IDictionary<string, object?>?>? Defaults { get; }

        public Theme Theme { get; }
        
        public Icons Icons { get; }

        public event Action<bool> OnRTLChange;

        public event Action<Theme> OnThemeChange;

        public void ToggleTheme()
        {
            Theme.Dark = !Theme.Dark;
            OnThemeChange?.Invoke(Theme);
        }
    }
}
