namespace Masa.Blazor;

public class BreakpointChangedEventArgs(Breakpoints breakpoint, bool breakpointChanged, bool mobile, bool mobileChanged)
    : MobileChangedEventArgs(mobile, mobileChanged)
{
    public Breakpoints Breakpoint { get; } = breakpoint;

    internal bool BreakpointChanged { get; } = breakpointChanged;
}