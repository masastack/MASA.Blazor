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
    /// The top value of RootMargin.
    /// For details: https://developer.mozilla.org/en-US/docs/Web/API/IntersectionObserver/rootMargin
    /// </summary>
    public string RootMarginTop { get; set; } = "0px";

    /// <summary>
    /// The right value of RootMargin.
    /// For details: https://developer.mozilla.org/en-US/docs/Web/API/IntersectionObserver/rootMargin
    /// </summary>
    public string RootMarginRight { get; set; } = "0px";

    /// <summary>
    /// The bottom value of RootMargin.
    /// For details: https://developer.mozilla.org/en-US/docs/Web/API/IntersectionObserver/rootMargin
    /// </summary>
    public string RootMarginBottom { get; set; } = "0px";

    /// <summary>
    /// The left value of RootMargin.
    /// For details: https://developer.mozilla.org/en-US/docs/Web/API/IntersectionObserver/rootMargin
    /// </summary>
    public string RootMarginLeft { get; set; } = "0px";

    /// <summary>
    /// The margin value of RootMargin that will be automatically calculated.
    /// For example, if you set <see cref="RootMarginTop"/> to 64px,
    /// the value of RootMargin bottom will be `calc(64px - 100%)`.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AutoRootMargin AutoRootMargin { get; set; }

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
