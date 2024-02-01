namespace Masa.Blazor;

public class InfiniteScrollLoadEventArgs : EventArgs
{
    /// <summary>
    /// The load status of infinite scroll.
    /// </summary>
    public InfiniteScrollLoadStatus Status { get; set; }
}

public enum InfiniteScrollLoadStatus
{
    /// <summary>
    /// Content was loaded successfully.
    /// </summary>
    Ok,
    /// <summary>
    /// Something went wrong when loading content.
    /// </summary>
    Error,
    /// <summary>
    /// There is no more content to load.
    /// </summary>
    Empty,
    /// <summary>
    /// Content is currently loading.
    /// This status should only be set internally by the component.
    /// </summary>
    Loading
}
