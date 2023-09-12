namespace Masa.Blazor;

/// <summary>
/// The init options of IntersectionObserver, more info see:
/// https://developer.mozilla.org/en-US/docs/Web/API/IntersectionObserver/IntersectionObserver
/// </summary>
public class IntersectionObserverInit
{
    public IntersectionObserverInit()
    {
    }

    public IntersectionObserverInit(params double[] threshold)
    {
        Threshold = threshold;
    }

    public IntersectionObserverInit(bool once)
    {
        Once = once;
    }

    /// <summary>
    /// An Element or Document object which is an ancestor of the intended target,
    /// whose bounding rectangle will be considered the viewport.
    /// Any part of the target not visible in the visible area of the root
    /// is not considered visible.
    /// </summary>
    public string? RootMargin { get; set; } = "0px";

    /// <summary>
    /// A string which specifies a set of offsets to add to the root's bounding_box
    /// when calculating intersections,
    /// effectively shrinking or growing the root for calculation purposes.
    /// </summary>
    public string? RootSelector { get; set; }

    /// <summary>
    /// Either an array of numbers between 0.0 and 1.0,
    /// specifying a ratio of intersection area to total bounding box area
    /// for the observed target.
    /// </summary>
    public double[] Threshold { get; set; } = { };

    /// <summary>
    /// The callback of intersection is only invoked once,
    /// the first time the element is visible.
    /// </summary>
    public bool Once { get; set; }
}
