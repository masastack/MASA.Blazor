namespace Masa.Blazor;

public class BreakpointChangedEventArgs : MobileChangedEventArgs
{
    /// <summary>
    /// Indicates whether the value of Mobile property has changed. First time is always false.
    /// </summary>
    public bool MobileChanged { get; set; }
}
