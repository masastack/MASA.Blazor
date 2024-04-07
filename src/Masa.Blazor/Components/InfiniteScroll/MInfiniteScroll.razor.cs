namespace Masa.Blazor;

public partial class MInfiniteScroll : BDomComponentBase
{
    [Inject] protected I18n I18n { get; set; } = null!;

    [Parameter, EditorRequired] public EventCallback<InfiniteScrollLoadEventArgs> OnLoad { get; set; }

    /// <summary>
    /// The parent element that has overflow style.
    /// </summary>
    [Parameter, EditorRequired]
    [MasaApiParameter("window")]
    public string? Parent { get; set; } = "window";

    [Parameter] public string? Color { get; set; }

    [Parameter]
    public bool Manual
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    [Parameter] [MasaApiParameter(250)] public StringNumber Threshold { get; set; } = 250;

    [Parameter]
    public RenderFragment<(InfiniteScrollLoadStatus Status, EventCallback OnLoad)>? ChildContent { get; set; }

    [Parameter] public string? EmptyText { get; set; }

    [Parameter] public string? LoadingText { get; set; }

    [Parameter] public string? LoadMoreText { get; set; }

    [Parameter] public string? ErrorText { get; set; }

    [Parameter] public RenderFragment? EmptyContent { get; set; }

    [Parameter] public RenderFragment<Func<Task>>? ErrorContent { get; set; }

    [Parameter] public RenderFragment? LoadingContent { get; set; }

    [Parameter] public RenderFragment<Func<Task>>? LoadMoreContent { get; set; }

    private bool _isAttached;
    private bool _dirtyLoad;
    private bool _firstRendered;
    private InfiniteScrollLoadStatus _loadStatus;
    private string? _prevParent;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (Manual)
        {
            _loadStatus = InfiniteScrollLoadStatus.Ok;
        }
    }

    public override Task SetParametersAsync(ParameterView parameters)
    {
        EmptyText ??= I18n.T("$masaBlazor.infiniteScroll.emptyText");
        ErrorText ??= I18n.T("$masaBlazor.infiniteScroll.errorText");
        LoadingText ??= I18n.T("$masaBlazor.infiniteScroll.loadingText");
        LoadMoreText ??= I18n.T("$masaBlazor.infiniteScroll.loadMoreText");

        return base.SetParametersAsync(parameters);
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (_prevParent != Parent)
        {
            _prevParent = Parent;
            if (!_firstRendered || !Manual) return;
            await AddScrollListenerAndLoadAsync();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (Manual)
            {
                await DoLoadMore();
                StateHasChanged();
            }
            else
            {
                _firstRendered = true;
                await AddScrollListenerAndLoadAsync();
            }
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task AddScrollListenerAndLoadAsync()
    {
        AddScrollListener();
        await ScrollCallback();
    }

    protected override void SetComponentCss()
    {
        CssProvider
            .Apply(cssBuilder => { cssBuilder.Add("m-infinite-scroll"); });
    }

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher.Watch<bool>(nameof(Manual), ManualChangeCallback);
    }

    private void ManualChangeCallback(bool val)
    {
        if (!val)
        {
            AddScrollListener();
        }
    }

    /// <summary>
    /// Reset and invoke <see cref="OnLoad"/> again.
    /// </summary>
    [MasaApiPublicMethod]
    public async Task ResetAsync() => await DoLoadMore();

    private void AddScrollListener()
    {
        if (!_firstRendered || _isAttached || string.IsNullOrWhiteSpace(Parent)) return;

        _isAttached = true;
        _ = Js.AddHtmlElementEventListener(Parent, "scroll", ScrollCallback, false,
            new EventListenerExtras(0, 0)
            {
                Key = Ref.Id
            });
    }

    private async Task ScrollCallback()
    {
        if (Parent is null || Manual || !OnLoad.HasDelegate || _loadStatus == InfiniteScrollLoadStatus.Empty)
        {
            return;
        }

        if (_loadStatus is InfiniteScrollLoadStatus.Error or InfiniteScrollLoadStatus.Empty
            or InfiniteScrollLoadStatus.Loading)
        {
            return;
        }

        // OPTIMIZE: Combine scroll event and the following js interop.
        var exceeded = await JsInvokeAsync<bool>(JsInteropConstants.CheckIfThresholdIsExceededWhenScrolling, Ref,
            Parent,
            Threshold.ToDouble());

        if (!exceeded)
        {
            return;
        }

        await DoLoadMore();

        _ = InvokeAsync(StateHasChanged);
    }

    private async Task DoLoadMore()
    {
        _dirtyLoad = true;
        _loadStatus = InfiniteScrollLoadStatus.Loading;

        var eventArgs = new InfiniteScrollLoadEventArgs();

        try
        {
            await OnLoad.InvokeAsync(eventArgs);
            _loadStatus = eventArgs.Status;
        }
        catch (Exception e)
        {
            _loadStatus = InfiniteScrollLoadStatus.Error;

            Logger.LogWarning(e, "Failed to load more");
            StateHasChanged();
        }
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        if (Parent is null)
        {
            return;
        }

        await Js.RemoveHtmlElementEventListener(Parent, "scroll", key: Ref.Id);
    }
}