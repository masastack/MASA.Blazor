namespace Masa.Blazor.Presets.PageStack;

public class StackPageData(string absolutePath, int id)
{
    public int Id { get; } = id;

    public string AbsolutePath { get; private set; } = absolutePath;

    /// <summary>
    /// Indicates whether the current page is already on the stack.
    /// From the component point of view, indicates whether the
    /// Dialog component is displayed. 
    /// </summary>
    public bool Stacked { get; internal set; } = true;

    /// <summary>
    /// The selector for the content area below the AppBar,
    /// which is also the topmost scrollable area.
    /// </summary>
    public string Selector => $"[page-stack-id=\"{Id}\"] .m-page-stack-item__content";

    public object? State { get; private set; }

    public void UpdateState(object? state) => State = state;

    public void UpdatePath(string absolutePath) => AbsolutePath = absolutePath;

    private bool _active;

    public void Activate()
    {
        if (_active)
        {
            return;
        }

        _active = true;

        ActiveChanged?.Invoke(this, new PageActiveStateEventArgs(true));
    }

    public void Deactivate()
    {
        if (!_active)
        {
            return;
        }

        _active = false;

        ActiveChanged?.Invoke(this, new PageActiveStateEventArgs(false));
    }

    public event EventHandler<PageActiveStateEventArgs>? ActiveChanged;
}

public class PageActiveStateEventArgs(bool active) : EventArgs
{
    public bool Active { get; } = active;
}