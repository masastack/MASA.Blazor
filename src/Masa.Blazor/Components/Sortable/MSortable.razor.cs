namespace Masa.Blazor;

public partial class MSortable<TItem> : MasaComponentBase
{
    [Parameter] public RenderFragment<TItem>? ChildContent { get; set; }

    [Parameter] [EditorRequired] public IEnumerable<TItem> Items { get; set; } = Enumerable.Empty<TItem>();

    [Parameter] public IEnumerable<string>? Order { get; set; }

    [Parameter] public EventCallback<IEnumerable<string>> OrderChanged { get; set; }

    [Parameter] [EditorRequired] public Func<TItem, string> ItemKey { get; set; } = default!;

    [Parameter] [MasaApiParameter("div")] public string? Tag { get; set; } = "div";

    [Parameter] [MasaApiParameter("div")] public string? ItemTag { get; set; } = "div";

    /// <summary>
    /// ms, animation speed moving items when sorting, `0` — without animation
    /// </summary>
    [Parameter]
    public int Animation { get; set; }

    /// <summary>
    /// Class name for the chosen item, only accept a single class
    /// </summary>
    [Parameter]
    [MasaApiParameter("sortable-chosen")]
    public string? ChosenClass { get; set; } = "sortable-chosen";

    /// <summary>
    /// time in milliseconds to define when the sorting should start
    /// </summary>
    [Parameter]
    public int Delay { get; set; }

    /// <summary>
    /// Only delay if user is using touch
    /// </summary>
    [Parameter]
    public bool DelayOnTouchOnly { get; set; }

    /// <summary>
    /// Class name for the dragging item, only accept a single class
    /// </summary>
    [Parameter]
    [MasaApiParameter("sortable-drag")]
    public string? DragClass { get; set; } = "sortable-drag";

    /// <summary>
    /// Easing for animation.
    /// </summary>
    [Parameter]
    public string? Easing { get; set; }

    /// <summary>
    /// distance mouse must be from empty sortable to insert drag element into it
    /// </summary>
    [Parameter]
    [MasaApiParameter(5)]
    public int EmptyInsertThreshold { get; set; } = 5;

    /// <summary>
    /// Class name for the cloned DOM Element when using forceFallback
    /// </summary>
    [Parameter]
    [MasaApiParameter("sortable-fallback")]
    public string? FallbackClass { get; set; } = "sortable-fallback";

    /// <summary>
    /// Selectors that do not lead to dragging
    /// </summary>
    [Parameter]
    public string? Filter { get; set; }

    /// <summary>
    /// Ignore the HTML5 DnD behaviour and force the fallback to be used
    /// </summary>
    [Parameter]
    public bool ForceFallback { get; set; }

    /// <summary>
    /// Class name for the drop placeholder, only accept a single class
    /// </summary>
    [Parameter]
    [MasaApiParameter("sortable-ghost")]
    public string? GhostClass { get; set; } = "sortable-ghost";

    /// <summary>
    /// To drag elements from one list into another, both lists must have the same group value.
    /// You can also define whether lists can give away, give and keep a copy (clone), and receive elements.
    /// </summary>
    [Parameter]
    public string? Group { get; set; }

    /// <summary>
    /// Drag handle selector within list items
    /// </summary>
    [Parameter]
    public string? Handle { get; set; }

    /// <summary>
    /// Will always use inverted swap zone if set to true
    /// </summary>
    [Parameter]
    public bool InvertSwap { get; set; }

    /// <summary>
    /// Threshold of the inverted swap zone
    /// </summary>
    [Parameter]
    public int? InvertedSwapThreshold { get; set; }

    /// <summary>
    /// Call `event.preventDefault()` when triggered `filter` event
    /// </summary>
    [Parameter]
    [MasaApiParameter(true)]
    public bool PreventOnFilter { get; set; } = true;

    /// <summary>
    /// Allows elements from specific groups to be dragged into current list
    /// </summary>
    [Parameter]
    [MasaApiParameter]
    public IEnumerable<string>? GroupPulls { get; set; }

    /// <summary>
    /// An array of group that allows elements to be dropped into
    /// </summary>
    [Parameter]
    [MasaApiParameter]
    public IEnumerable<string>? GroupPuts { get; set; }

    /// <summary>
    /// Remove the clone element when it is not showing, rather than just hiding it
    /// </summary>
    [Parameter]
    [MasaApiParameter(true)]
    public bool RemoveCloneOnHide { get; set; } = true;

    /// <summary>
    /// Threshold of the swap zone
    /// </summary>
    [Parameter]
    [MasaApiParameter(1)]
    public int SwapThreshold { get; set; } = 1;

    private Block _block = new("m-sortable");
    private IEnumerable<TItem>? _prevItems;
    private int _prevItemsCount;

    private DotNetObjectReference<SortableJSInteropHandle>? _sortableJSInteropHandle;
    private SortableJSObjectReference? _jsObjectReference;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _prevItems = Items;
        _prevItemsCount = Items.Count();

        _sortableJSInteropHandle = DotNetObjectReference.Create(new SortableJSInteropHandle(this));
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items.ThrowIfNull(ComponentName);
        ItemKey.ThrowIfNull(ComponentName);

        if (!Equals(_prevItems, Items))
        {
            _prevItems = Items;
            _prevItemsCount = Items.Count();
            RefreshOrder();
        }
        else
        {
            var itemsCount = Items.Count();
            if (_prevItemsCount != itemsCount)
            {
                _prevItemsCount = itemsCount;
                RefreshOrder();
            }
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            if (_sortableJSInteropHandle is null)
            {
                return;
            }

            _jsObjectReference = await SortableJSModule.InitAsync(Ref, GenOptions(), Order, _sortableJSInteropHandle);
        }
    }

    private SortableOptions GenOptions()
    {
        return new SortableOptions()
        {
            Animation = Animation,
            ChosenClass = ChosenClass,
            Delay = Delay,
            DelayOnTouchOnly = DelayOnTouchOnly,
            DragClass = DragClass,
            Easing = Easing,
            EmptyInsertThreshold = EmptyInsertThreshold,
            FallbackClass = FallbackClass,
            Filter = Filter,
            ForceFallback = ForceFallback,
            GhostClass = GhostClass,
            Group = new(Group, GroupPulls, GroupPuts),
            Handle = Handle,
            InvertSwap = InvertSwap,
            InvertedSwapThreshold = InvertedSwapThreshold,
            PreventOnFilter = PreventOnFilter,
            RemoveCloneOnHide = RemoveCloneOnHide,
            SwapThreshold = SwapThreshold,
        };
    }

    // TODO： internal 
    public async ValueTask UpdateOrder(IEnumerable<string> order)
    {
        await OrderChanged.InvokeAsync(order);
    }

    private void RefreshOrder()
    {
        if (_jsObjectReference is null)
        {
            return;
        }

        NextTick(async () =>
        {
            var order = await _jsObjectReference.GetOrderAsync();
            await OrderChanged.InvokeAsync(order);
        });
    }
}
