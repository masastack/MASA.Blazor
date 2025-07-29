using Masa.Blazor.Components.Sortable;
using Masa.Blazor.Utils;

namespace Masa.Blazor;

public abstract class MSortableProviderBase<TItem> : MasaComponentBase, ISortable
{
    [Inject] private SortableJSModule SortableJSModule { get; set; } = null!;

    [Parameter] [EditorRequired] public IEnumerable<TItem> Items { get; set; } = Enumerable.Empty<TItem>();

    [Parameter] public List<string>? Order { get; set; }

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
    [MasaApiParameter(".ignore-elements")]
    public string? Filter { get; set; } = ".ignore-elements";

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
    [MasaApiParameter(".handle")]
    public virtual string? Handle { get; set; } = ".handle";

    /// <summary>
    /// The selector of the list items that are ignored
    /// </summary>
    [Parameter]
    public string? Ignore { get; set; }

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
    private HashSet<string> _prevItemKeys = [];
    private string[] _prevOrder = [];

    private DotNetObjectReference<SortableJSInteropHandle>? _sortableJSInteropHandle;
    private SortableJSObjectReference? _jsObjectReference;

    private RenderRateLimiter? _renderRateLimiter;

    protected abstract string? ContainerSelector { get; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _prevItems = Items;
        _prevItemKeys = GetItemKeys();
        _renderRateLimiter = new RenderRateLimiter(
            $"{ComponentName} is rendering too frequently. Check the Items parameter to ensure its reference doesn't change frequently.");

        _sortableJSInteropHandle = DotNetObjectReference.Create(new SortableJSInteropHandle(this));
    }

    private HashSet<string> GetItemKeys() => new(Items.Select(u => ItemKey(u)));

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher.Watch<bool>(nameof(Disabled),
            val => { _jsObjectReference?.InvokeVoidAsync("option", "disabled", val); });
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
            _renderRateLimiter?.RecordRender();
            SortByOrder();
        }
        else
        {
            var keys = GetItemKeys();
            if (!_prevItemKeys.SetEquals(keys))
            {
                _prevItemKeys = keys;
                SortByOrder();
            }
        }

        if (Order is not null && !_prevOrder.SequenceEqual(Order))
        {
            _prevOrder = Order.ToArray();
            _ = _jsObjectReference?.SortAsync(Order, false);
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        await InitAsync();
    }

    private async Task InitAsync()
    {
        if (_jsObjectReference is not null)
        {
            return;
        }

        if (_sortableJSInteropHandle is null || ContainerSelector is null)
        {
            return;
        }

        _jsObjectReference =
            await SortableJSModule.InitAsync(ContainerSelector, GenOptions(), Order, _sortableJSInteropHandle);
    }

    private SortableOptions GenOptions()
    {
        return new SortableOptions()
        {
            Animation = Animation,
            ChosenClass = ChosenClass,
            Delay = Delay,
            DelayOnTouchOnly = DelayOnTouchOnly,
            Ignore = Ignore,
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

    public ValueTask UpdateOrder(List<string> order)
    {
        _ = OrderChanged.InvokeAsync(order);
        return ValueTask.CompletedTask;
    }

    public async ValueTask HandleOnAdd(string key, List<string> order)
    {
        _ = OrderChanged.InvokeAsync(order);
        await OnAdd.InvokeAsync(key);
    }

    public async ValueTask HandleOnRemove(string key, List<string> order)
    {
        _ = OrderChanged.InvokeAsync(order);
        await OnRemove.InvokeAsync(key);
    }

    private void SortByOrder()
    {
        if (_jsObjectReference is null)
        {
            return;
        }

        NextTick(() => _ = _jsObjectReference.SortAsync(Order, false).ConfigureAwait(false));
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        _sortableJSInteropHandle?.Dispose();

        if (_jsObjectReference != null)
        {
            await _jsObjectReference.DisposeAsync();
            _jsObjectReference = null;
        }
    }
}