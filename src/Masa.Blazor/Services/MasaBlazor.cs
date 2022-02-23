namespace Masa.Blazor
{
    /// <summary>
    /// Cascading this will cause additional render,we may just cascading rtl in the feature
    /// </summary>
    public class MasaBlazor
    {
        private bool _rtl;

        public MasaBlazor(Breakpoint breakpoint, Application application)
        {
            Breakpoint = breakpoint;
            Application = application;
        }

        public bool RTL
        {
            get
            {
                return _rtl;
            }
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

        public event Action<bool> OnRTLChange;
    }
}
