namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackGoBackToPageEventArgs : EventArgs
{
    public PageStackGoBackToPageEventArgs(string absolutePath, object? state = null, string? replaceUri = null)
    {
        ArgumentNullException.ThrowIfNull(absolutePath);

        AbsolutePath = absolutePath;
        State = state;
        ReplaceUri = replaceUri;
    }

    public string AbsolutePath { get; }

    public object? State { get; }

    /// <summary>
    /// The URI to replace after going back to the <see cref="AbsolutePath"/> page.
    /// </summary>
    public string? ReplaceUri { get; set; }
}