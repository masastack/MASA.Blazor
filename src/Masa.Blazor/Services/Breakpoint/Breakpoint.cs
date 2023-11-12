﻿namespace Masa.Blazor;

public class Breakpoint
{
    private bool _prevMobile;

    public Breakpoint()
    {
    }

    public Breakpoint(Window window)
    {
        Window = window;
    }

    internal void SetWindow(Window window) => Window = window;

    /// <summary>
    /// Indicates that the breakpoint has been calculated. If not, the breakpoint will not work.
    /// For example, the <see cref="Mobile"/> may be not correct before the breakpoint is initialized.
    /// </summary>
    public bool Initialized { get; private set; }

    public bool Xs { get; private set; }

    public bool Sm { get; private set; }

    public bool Md { get; private set; }

    public bool Lg { get; private set; }

    public bool Xl { get; private set; }

    public bool XsOnly { get; private set; }

    public bool SmOnly { get; private set; }

    public bool SmAndDown { get; private set; }

    public bool SmAndUp { get; private set; }

    public bool MdOnly { get; private set; }

    public bool MdAndDown { get; private set; }

    public bool MdAndUp { get; private set; }

    public bool LgOnly { get; private set; }

    public bool LgAndDown { get; private set; }

    public bool LgAndUp { get; private set; }

    public bool XlOnly { get; private set; }

    public Breakpoints Name { get; private set; }

    public double Height { get; private set; }

    public double Width { get; private set; }

    public bool Mobile { get; private set; }

    public OneOf<Breakpoints, double> MobileBreakpoint { get; set; }

    public BreakpointThresholds Thresholds { get; set; }

    public double ScrollBarWidth { get; set; }

    [Obsolete("Use MasaBlazor.BreakpointChanged instead")]
    public event EventHandler<BreakpointChangedEventArgs> OnUpdate;

    internal Action<BreakpointChangedEventArgs> OnChanged;

    Window Window { get; set; }

    Document Document => Window.Document;

    public void Deconstruct(
        out double width,
        out bool mobile,
        out Breakpoints name,
        out OneOf<Breakpoints, double> mobileBreakpoint)
    {
        width = Width;
        mobile = Mobile;
        name = Name;
        mobileBreakpoint = MobileBreakpoint;
    }

    public async Task InitAsync()
    {
        await UpdateAsync();
        await Window.AddEventListenerAsync(
            "resize",
            UpdateAsync,
            new EventListenerOptions { Passive = true },
            new EventListenerExtras(debounce: 200));
    }

    private async Task UpdateAsync()
    {
        var height = await GetClientHeightAsync();
        var width = await GetClientWidthAsync();

        var xs = width < Thresholds.Xs;
        var sm = width < Thresholds.Sm && !xs;
        var md = width < (Thresholds.Md - ScrollBarWidth) && !(sm || xs);
        var lg = width < (Thresholds.Lg - ScrollBarWidth) && !(md || sm || xs);
        var xl = width >= (Thresholds.Lg - ScrollBarWidth);

        Height = height;
        Width = width;

        Xs = xs;
        Sm = sm;
        Md = md;
        Lg = lg;
        Xl = xl;

        XsOnly = xs;
        SmOnly = sm;
        SmAndDown = (xs || sm) && !(md || lg || xl);
        SmAndUp = !xs && (sm || md || lg || xl);
        MdOnly = md;
        MdAndDown = (xs || sm || md) && !(lg || xl);
        MdAndUp = !(xs || sm) && (md || lg || xl);
        LgOnly = lg;
        LgAndDown = (xs || sm || md || lg) && !xl;
        LgAndUp = !(xs || sm || md) && (lg || xl);
        XlOnly = xl;

        if (xs)
        {
            Name = Breakpoints.Xs;
        }
        else if (sm)
        {
            Name = Breakpoints.Sm;
        }
        else if (md)
        {
            Name = Breakpoints.Md;
        }
        else if (lg)
        {
            Name = Breakpoints.Lg;
        }
        else
        {
            Name = Breakpoints.Xl;
        }

        if (MobileBreakpoint.IsT1)
        {
            Mobile = width < MobileBreakpoint.AsT1;
        }
        else
        {
            var current = Name;
            var max = MobileBreakpoint.AsT0;

            Mobile = current <= max;
        }

        var eventArgs = new BreakpointChangedEventArgs();

        Initialized = true;

        if (_prevMobile != Mobile)
        {
            _prevMobile = Mobile;
            eventArgs.MobileChanged = true;
        }

        OnUpdate?.Invoke(this, eventArgs);

        OnChanged?.Invoke(eventArgs);
    }

    private async Task<double> GetClientHeightAsync()
    {
        var clientHeight = await Document.GetClientHeightAsync();
        var innerHeight = await Window.GetInnerHeightAsync();

        return Math.Max(clientHeight ?? 0, innerHeight ?? 0);
    }

    private async Task<double> GetClientWidthAsync()
    {
        var clientWidth = await Document.GetClientWidthAsync();
        var innerWidth = await Window.GetInnerWidthAsync();

        return Math.Max(clientWidth ?? 0, innerWidth ?? 0);
    }
}
