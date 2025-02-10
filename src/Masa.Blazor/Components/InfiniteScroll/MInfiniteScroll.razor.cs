namespace Masa.Blazor;

public partial class MInfiniteScroll : MasaComponentBase
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

    [Parameter] public bool Manual { get; set; }

    // TODO: [v2] no need to use OneOf here, just use int
    [Parameter] [MasaApiParameter(100)] public StringNumber Threshold { get; set; } = 100;

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
    private bool _enable = true;

    private bool _prevManual;
    private string? _prevParent;
    private StringNumber? _prevThreshold;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _prevThreshold = Threshold;

        if (Manual)
        {
            _loadStatus = InfiniteScrollLoadStatus.Ok;
            _enable = false;
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

        if (_prevThreshold != Threshold)
        {
            _prevThreshold = Threshold;
            if (!_firstRendered || !Manual) return;
            SyncThresholdToJS();
        }

        if (_prevManual != Manual)
        {
            _prevManual = Manual;
            _enable = !Manual;
            if (!_firstRendered) return;
            SyncEnableToJS();
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
        await AddScrollListener();
        await OnScrollInternal();
    }

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "m-infinite-scroll";
    }

    /// <summary>
    /// Reset and invoke <see cref="OnLoad"/> again.
    /// </summary>
    [MasaApiPublicMethod]
    public async Task ResetAsync()
    {
        _enable = !Manual;
        SyncEnableToJS();
        await DoLoadMore();
    }

    /// <summary>
    /// Reset internal status to <see cref="InfiniteScrollLoadStatus.Ok"/>
    /// </summary>
    [MasaApiPublicMethod]
    public void ResetStatus()
    {
        _loadStatus = InfiniteScrollLoadStatus.Ok;
        _enable = !Manual;
        SyncEnableToJS();
        StateHasChanged();
    }

    private IJSObjectReference? _jsObjectReference;

    private async Task AddScrollListener()
    {
        if (!_firstRendered || _isAttached || string.IsNullOrWhiteSpace(Parent)) return;

        _isAttached = true;

        _jsObjectReference =
            await Js.InvokeAsync<IJSObjectReference>(JsInteropConstants.RegisterInfiniteScrollJSInterop,
                Ref,
                Parent,
                Threshold.ToDouble(),
                _enable,
                DotNetObjectReference.Create(this));
    }

    [JSInvokable]
    public async Task OnScrollInternal()
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

        await DoLoadMore();
        StateHasChanged();
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

            if (_loadStatus == InfiniteScrollLoadStatus.Empty)
            {
                _enable = false;
                SyncEnableToJS();
            }
        }
        catch (Exception e)
        {
            _loadStatus = InfiniteScrollLoadStatus.Error;

            Logger.LogWarning(e, "Failed to load more");
            StateHasChanged();
        }
    }

    private void SyncEnableToJS()
    {
        _ = _jsObjectReference.TryInvokeVoidAsync("updateEnable", _enable);
    }

    private void SyncThresholdToJS()
    {
        _ = _jsObjectReference.TryInvokeVoidAsync("updateThreshold", Threshold.ToDouble());
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        if (Parent is null)
        {
            return;
        }

        if (_jsObjectReference is not null)
        {
            await _jsObjectReference.InvokeVoidAsync("dispose");
            await _jsObjectReference.TryDisposeAsync();
        }
    }
}