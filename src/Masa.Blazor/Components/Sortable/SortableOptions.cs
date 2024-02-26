namespace Masa.Blazor;

public record SortableGroup(string Name, IEnumerable<string>? Pulls, IEnumerable<string>? Puts);

public class SortableOptions
{
    /// <summary>
    /// ms, animation speed moving items when sorting, `0` — without animation
    /// </summary>
    public int Animation { get; set; }

    /// <summary>
    /// Class name for the chosen item, only accept a single class
    /// </summary>
    public string? ChosenClass { get; set; } = "sortable-chosen";

    /// <summary>
    /// time in milliseconds to define when the sorting should start
    /// </summary>
    public int Delay { get; set; }

    /// <summary>
    /// Only delay if user is using touch
    /// </summary>
    public bool DelayOnTouchOnly { get; set; }
    
    /// <summary>
    /// Disables the sortable if set to true.
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// Specifies which items inside the element should be draggable
    /// </summary>
    public string? Draggable { get; set; }

    /// <summary>
    /// Class name for the dragging item, only accept a single class
    /// </summary>
    public string? DragClass { get; set; } = "sortable-drag";

    /// <summary>
    /// Easing for animation.
    /// </summary>
    public string? Easing { get; set; }

    /// <summary>
    /// px, distance mouse must be from empty sortable to insert drag element into it
    /// </summary>
    public int EmptyInsertThreshold { get; set; } = 5;

    /// <summary>
    /// Class name for the cloned DOM Element when using forceFallback
    /// </summary>
    public string? FallbackClass { get; set; } = "sortable-fallback";

    /// <summary>
    /// Selectors that do not lead to dragging
    /// </summary>
    public string? Filter { get; set; }

    /// <summary>
    /// Ignore the HTML5 DnD behaviour and force the fallback to be used
    /// </summary>
    public bool ForceFallback { get; set; }

    /// <summary>
    /// Class name for the drop placeholder, only accept a single class
    /// </summary>
    public string? GhostClass { get; set; } = "sortable-ghost";

    /// <summary>
    /// To drag elements from one list into another, both lists must have the same group value.
    /// You can also define whether lists can give away, give and keep a copy (clone), and receive elements.
    /// </summary>
    public SortableGroup? Group { get; set; }

    /// <summary>
    /// Drag handle selector within list items
    /// </summary>
    public string? Handle { get; set; }

    /// <summary>
    /// Will always use inverted swap zone if set to true
    /// </summary>
    public bool InvertSwap { get; set; }

    /// <summary>
    /// Threshold of the inverted swap zone
    /// </summary>
    public double? InvertedSwapThreshold { get; set; }

    /// <summary>
    /// Call `event.preventDefault()` when triggered `filter` event
    /// </summary>
    public bool PreventOnFilter { get; set; } = true;

    /// <summary>
    /// Remove the clone element when it is not showing, rather than just hiding it
    /// </summary>
    public bool RemoveCloneOnHide { get; set; } = true;

    /// <summary>
    /// Threshold of the swap zone
    /// </summary>
    public double SwapThreshold { get; set; } = 1;
}