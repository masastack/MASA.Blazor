namespace Masa.Blazor.Presets;

public class ModalActionEventArgs
{
    public ModalActionEventArgs()
    {
        Cancelled = false;
    }

    public bool Cancelled { get; private set; }

    /// <summary>
    /// Cancel the next operation.
    /// </summary>
    /// <remarks>
    /// The form would be reset when Form's OnOk is triggered,
    /// you can call this method to prevent this behavior.
    /// </remarks>
    public void Cancel()
    {
        Cancelled = true;
    }
}