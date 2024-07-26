namespace Masa.Blazor;

public class MobileChangedEventArgs(bool mobile, bool mobileChanged) : EventArgs
{
    public bool Mobile { get; } = mobile;

    /// <summary>
    /// Indicates whether the value of Mobile property has changed. First time is always false.
    /// </summary>
    internal bool MobileChanged { get; } = mobileChanged;
}