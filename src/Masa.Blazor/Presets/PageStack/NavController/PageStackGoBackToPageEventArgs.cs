namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackGoBackToPageEventArgs : EventArgs
{
    public PageStackGoBackToPageEventArgs(string absolutePath, object? state = null)
    {
        ArgumentNullException.ThrowIfNull(absolutePath);

        AbsolutePath = absolutePath;
        State = state;
    }

    public string AbsolutePath { get; }

    public object? State { get; }
}