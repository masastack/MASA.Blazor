namespace Masa.Blazor.Presets;

public class ModalActionEventArgs
{
    public ModalActionEventArgs()
    {
        IsCanceled = false;
    }

    public bool IsCanceled { get; private set; }

    /// <summary>
    /// Cancel the next operation.
    /// </summary>
    /// <remarks>
    /// The form would be reset when Form's OnSave is triggered,
    /// you can call this method to prevent this behavior.
    /// </remarks>
    public void Cancel()
    {
        IsCanceled = true;
    }
}