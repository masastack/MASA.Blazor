namespace Masa.Blazor;

public class BreakpointChangedEventArgs : EventArgs
{
    /// <summary>
    /// Determines whether the breakpoint is calculated for the first time. Typically used in layout components.
    /// </summary>
    public bool FirstCalculation { get; set; }

    /// <summary>
    /// Determines whether the mobile breakpoint has changed. First time is always false.
    /// </summary>
    public bool MobileChanged { get; set; }
}
