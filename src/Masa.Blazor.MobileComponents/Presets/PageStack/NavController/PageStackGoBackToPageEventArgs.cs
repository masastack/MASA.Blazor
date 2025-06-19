namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackGoBackToPageEventArgs : EventArgs
{
    public PageStackGoBackToPageEventArgs(string absolutePath, object? state = null, string? replaceUri = null, bool disableTransition = false)
    {
        ArgumentNullException.ThrowIfNull(absolutePath);

        AbsolutePath = absolutePath;
        State = state;
        ReplaceUri = replaceUri;
        DisableTransition = disableTransition;
    }

    public string AbsolutePath { get; }

    public object? State { get; }

    /// <summary>
    /// The URI to replace after going back to the <see cref="AbsolutePath"/> page.
    /// </summary>
    public string? ReplaceUri { get; set; }
    
    internal bool DisableTransition { get; }
}