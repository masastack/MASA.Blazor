namespace Masa.Blazor.Presets.PageStack;

public class StackPageData
{
    public StackPageData(string absolutePath)
    {
        Stacked = true;
        AbsolutePath = absolutePath;
    }

    public Guid Id { get; init; } = Guid.NewGuid();

    public string AbsolutePath { get; private set; }

    /// <summary>
    /// Indicates whether the current page is already on the stack.
    /// From the component point of view, indicates whether the
    /// Dialog component is displayed. 
    /// </summary>
    public bool Stacked { get; set; }

    public string Selector { get; set; }

    public object? State { get; set; }

    public void UpdateState(object? state) => State = state;

    public void UpdatePath(string absolutePath) => AbsolutePath = absolutePath;

    private bool _active;

    public void Activate()
    {
        if (_active)
        {
            return;
        }

        ActiveChanged?.Invoke(this, new PageActiveStateEventArgs(true));
    }

    public void Deactivate()
    {
        if (!_active)
        {
            return;
        }

        ActiveChanged?.Invoke(this, new PageActiveStateEventArgs(false));
    }

    public event EventHandler<PageActiveStateEventArgs>? ActiveChanged;
}

public class PageActiveStateEventArgs : EventArgs
{
    public PageActiveStateEventArgs(bool active)
    {
        Active = active;
    }

    public bool Active { get; }
}