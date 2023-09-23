namespace Masa.Blazor
{
    /// <summary>
    /// Cascading this will cause additional render,we may just cascading rtl in the feature
    /// </summary>
    public class MasaBlazor
    {
        public MasaBlazor(
            Breakpoint breakpoint,
            Application application,
            Icons icons,
            SSROptions? ssr = null,
            IDictionary<string, IDictionary<string, object?>?>? defaults = null)
        {
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
            Icons = icons;
            SSR = ssr;
            Defaults = defaults;
        }

        public Application Application { get; }

        public Breakpoint Breakpoint { get; }

        public IDictionary<string, IDictionary<string, object?>?>? Defaults { get; }

        public Icons Icons { get; }

        public SSROptions? SSR { get; }

        /// <summary>
        /// An event that fires when the breakpoint has changed.
        /// </summary>
        public event EventHandler<BreakpointChangedEventArgs>? BreakpointChanged;

        /// <summary>
        /// An event that fires when the the value of Mobile property from <see cref="Breakpoint"/> has changed.
        /// </summary>
        public event EventHandler<MobileChangedEventArgs>? MobileChanged;
    }
}
