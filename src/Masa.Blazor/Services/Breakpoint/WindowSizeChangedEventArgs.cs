namespace Masa.Blazor;

public class WindowSizeChangedEventArgs(Breakpoints breakpoint, bool breakpointChanged, bool mobile, bool mobileChanged)
    : BreakpointChangedEventArgs(breakpoint, breakpointChanged, mobile, mobileChanged)
{
}