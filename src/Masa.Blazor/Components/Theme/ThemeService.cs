namespace Masa.Blazor
{
    public class ThemeService : IThemeService
    {
        private List<BThemeProvider> _handles { get; set; } = new();

        private bool _dark;

        public bool _rtl;

        private Theme _theme;

        public ThemeService(Theme theme, bool dark,bool rtl)
        { 
            _theme = theme;
            _dark = dark;
            _rtl = rtl;
        }

        public event Action<Theme>? ThemeChanged;

        public event Action<bool>? DarkChanged;

        public event Action<bool>? RTLChanged;

        public Theme Theme
        {
            get => _theme;
            set
            {
                _theme = value;
                UpdateTheme();
                ThemeChanged?.Invoke(_theme);
            }
        }


        public bool Dark
        {
            get => _dark;
            set
            {
                _dark = value;
                UpdateTheme();
                DarkChanged?.Invoke(_dark);
            }
        }

        public bool RTL
        {
            get => _rtl;
            set
            {
                _rtl = value;
                UpdateTheme();
                RTLChanged?.Invoke(_rtl);
            }
        }

        public void UpdateTheme()
        {
            var handles = _handles.Where(s => !s.Default);
            bool flag = false;
            if (!handles.Any())
            {
                flag = true;
                handles = _handles;
            }
            foreach (var handle in handles)
            {
                handle.UpdateTheme(flag);
            }
        }

        public void Register(BThemeProvider handle)
        {
            if (_handles.IndexOf(handle) == -1)
            {
                _handles.Add(handle);
            }
        }

        public void UnRegister(BThemeProvider handle)
        {
            _handles.Remove(handle);
        }
    }
}
