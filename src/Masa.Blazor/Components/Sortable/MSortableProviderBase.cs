namespace Masa.Blazor;

public abstract class MSortableProviderBase<TItem> : MasaComponentBase, ISortable
{
    [Inject] private SortableJSModule SortableJSModule { get; set; } = null!;

    [Parameter] [EditorRequired] public IEnumerable<TItem> Items { get; set; } = Enumerable.Empty<TItem>();

    [Parameter]
    public List<string>? Order
    {
        get => GetValue<List<string>>();
        set => SetValue(value);
    }

    [Parameter] public EventCallback<List<string>> OrderChanged { get; set; }

    [Parameter] [EditorRequired] public Func<TItem, string> ItemKey { get; set; } = default!;

    [Parameter] public string? ItemClass { get; set; }

    [Parameter] public string? ItemStyle { get; set; }

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
    /// Disables the sortable if set to true.
    /// </summary>
    [Parameter]
    public bool Disabled
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    /// <summary>
    /// Specifies which items inside the element should be draggable
    /// </summary>
    [Parameter]
    public string? Draggable { get; set; }

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
    /// px, distance mouse must be from empty sortable to insert drag element into it
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
    public double? InvertedSwapThreshold { get; set; }

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
    public double SwapThreshold { get; set; } = 1;

    [Parameter] public EventCallback<string> OnAdd { get; set; }

    [Parameter] public EventCallback<string> OnRemove { get; set; }

    private IEnumerable<TItem>? _prevItems;
    private HashSet<string> _prevItemKeys;
    private List<string> _internalOrder;

    private DotNetObjectReference<SortableJSInteropHandle>? _sortableJSInteropHandle;
    private SortableJSObjectReference? _jsObjectReference;

    protected abstract string ContainerSelector { get; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _prevItems = Items;
        _prevItemKeys = GetItemKeys();

        _sortableJSInteropHandle = DotNetObjectReference.Create(new SortableJSInteropHandle(this));
    }

    private HashSet<string> GetItemKeys() => new(Items.Select(u => ItemKey(u)));

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher.Watch<bool>(nameof(Disabled),
                val =>
                {
                    Console.Out.WriteLine("[MSortableProviderBase] Disabled changed to " + val);
                    _jsObjectReference?.InvokeVoidAsync("option", "disabled", val);
                })
            .Watch<List<string>>(nameof(Order), val => { _ = _jsObjectReference?.SortAsync(val, false); });
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items.ThrowIfNull(ComponentName);
        ItemKey.ThrowIfNull(ComponentName);

        if (!Equals(_prevItems, Items))
        {
            _prevItems = Items;
            _prevItemKeys = GetItemKeys();
            RefreshOrder();
        }
        else
        {
            var keys = GetItemKeys();
            if (!_prevItemKeys.SetEquals(keys))
            {
                _prevItemKeys = keys;
                SortByInternalOrder();
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

            _jsObjectReference =
                await SortableJSModule.InitAsync(ContainerSelector, GenOptions(), Order, _sortableJSInteropHandle);
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
            Disabled = Disabled,
            Draggable = Draggable,
            DragClass = DragClass,
            Easing = Easing,
            EmptyInsertThreshold = EmptyInsertThreshold,
            FallbackClass = FallbackClass,
            Filter = Filter,
            ForceFallback = ForceFallback,
            GhostClass = GhostClass,
            Group = Group is null ? null : new(Group, GroupPulls, GroupPuts),
            Handle = Handle,
            InvertSwap = InvertSwap,
            InvertedSwapThreshold = InvertedSwapThreshold,
            PreventOnFilter = PreventOnFilter,
            RemoveCloneOnHide = RemoveCloneOnHide,
            SwapThreshold = SwapThreshold,
        };
    }

    private void UpdateOrderInternal(List<string> order)
    {
        _internalOrder = order;
        _ = OrderChanged.InvokeAsync(order);
    }

    public async ValueTask UpdateOrder(List<string> order)
    {
        UpdateOrderInternal(order);
    }

    public async ValueTask HandleOnAdd(string key, List<string> order)
    {
        UpdateOrderInternal(order);

        await OnAdd.InvokeAsync(key);
    }

    public async ValueTask HandleOnRemove(string key, List<string> order)
    {
        UpdateOrderInternal(order);

        await OnRemove.InvokeAsync(key);
    }

    private void RefreshOrder()
    {
        if (_jsObjectReference is null)
        {
            return;
        }

        NextTick(async () =>
        {
            var order = await _jsObjectReference.ToArrayAsync();
            UpdateOrderInternal(order);
        });
    }

    private void SortByInternalOrder()
    {
        if (_jsObjectReference is null)
        {
            return;
        }

        NextTick(() => _ = _jsObjectReference.SortAsync(_internalOrder, false));
    }
}