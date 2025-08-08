namespace Masa.Blazor.Presets.PageStack;

public class StackPageData(string absolutePath, int id)
{
    internal int PreviousPageId { get; private set; } = -1;
    
    public int Id { get; } = id;

    public string AbsolutePath { get; private set; } = absolutePath;

    /// <summary>
    /// Indicates whether the current page is already on the stack.
    /// From the component point of view, it indicates whether the
    /// Dialog component is displayed. 
    /// </summary>
    public bool Stacked { get; internal set; } = true;

    /// <summary>
    /// The selector for the content area below the AppBar,
    /// which is also the topmost scrollable area.
    /// </summary>
    public string Selector => $"[page-stack-id=\"{Id}\"] .m-page-stack-item__content";
    
    /// <summary>
    /// Determines whether to disable the transition
    /// when navigating to this page or delete it from the stack.
    /// </summary>
    public bool DisableTransition { get; set; }

    /// <summary>
    /// Gets or sets the state of the page.
    /// </summary>
    public object? State { get; private set; }

    public bool Active { get; private set; }
    
    internal bool ReadyToRender { get; set; }
    
    internal void UpdateState(object? state) => State = state;

    internal void UpdatePath(string absolutePath) => AbsolutePath = absolutePath;

    internal void Activate(int previousPageId = -1)
    {
        if (Active)
        {
            return;
        }
        
        PreviousPageId = previousPageId;

        Active = true;

        ActiveChanged?.Invoke(this, new PageActiveStateEventArgs(true));
    }

    internal void Deactivate()
    {
        if (!Active)
        {
            return;
        }

        Active = false;

        ActiveChanged?.Invoke(this, new PageActiveStateEventArgs(false));
    }

    internal event EventHandler<PageActiveStateEventArgs>? ActiveChanged;
}

public class PageActiveStateEventArgs(bool active) : EventArgs
{
    public bool Active { get; } = active;
}