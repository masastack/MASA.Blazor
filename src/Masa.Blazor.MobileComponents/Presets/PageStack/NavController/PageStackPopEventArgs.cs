namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackPopEventArgs : EventArgs
{
    public PageStackPopEventArgs(int delta, string? replaceUri = null, object? state = null, bool isStackEmpty = false, bool disableTransition = false)
    {
        if (delta < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(delta), "The delta must be greater than or equal to 1.");
        }

        Delta = delta;
        ReplaceUri = replaceUri;
        State = state;
        IsStackEmpty = isStackEmpty;
        DisableTransition = disableTransition;
    }

    /// <summary>
    /// The count of pages to pop.
    /// </summary>
    public int Delta { get; }

    public string? ReplaceUri { get; }

    public object? State { get; }

    /// <summary>
    /// True if the stack is empty after popping.
    /// </summary>
    public bool IsStackEmpty { get; }
    
    internal bool DisableTransition { get; }
}