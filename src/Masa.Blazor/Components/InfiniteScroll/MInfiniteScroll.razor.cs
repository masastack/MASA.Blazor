namespace Masa.Blazor;

public partial class MInfiniteScroll : BDomComponentBase
{
    [Inject] protected I18n I18n { get; set; } = null!;

    [Parameter, EditorRequired] public EventCallback<InfiniteScrollLoadEventArgs> OnLoad { get; set; }

    /// <summary>
    /// The parent element that has overflow style.
    /// </summary>
    [Parameter, EditorRequired]
    public OneOf<ElementReference, string>? Parent { get; set; }

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
    private string? _parentSelector;
    private InfiniteScrollLoadStatus _loadStatus;

    public override Task SetParametersAsync(ParameterView parameters)
    {
        EmptyText ??= I18n.T("$masaBlazor.infiniteScroll.emptyText");
        ErrorText ??= I18n.T("$masaBlazor.infiniteScroll.errorText");
        LoadingText ??= I18n.T("$masaBlazor.infiniteScroll.loadingText");
        LoadMoreText ??= I18n.T("$masaBlazor.infiniteScroll.loadMoreText");

        return base.SetParametersAsync(parameters);
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

    private async void ManualChangeCallback(bool val)
    {
        if (val)
        {
            await AddScrollListener();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await DoLoadMore();

            NextTick(async () => { await AddScrollListener(); });
            StateHasChanged();
        }
    }

    /// <summary>
    /// Reset and invoke <see cref="OnLoad"/> again.
    /// </summary>
    [MasaApiPublicMethod]
    public async Task ResetAsync() => await DoLoadMore();

    private async Task AddScrollListener()
    {
        if (!_isAttached && Parent is { Value: not null })
        {
            string? selector = null;

            if (Parent.Value.IsT0 && Parent.Value.AsT0.Id is not null)
            {
                selector = Parent.Value.AsT0.GetSelector();
            }
            else if (Parent.Value.IsT1)
            {
                selector = Parent.Value.AsT1;
            }

            if (selector is null)
            {
                return;
            }

            _isAttached = true;
            _parentSelector = selector;

            await Js.AddHtmlElementEventListener(selector, "scroll", OnScroll, false, new EventListenerExtras(0, 100));
        }
    }

    private async Task OnScroll()
    {
        if (_parentSelector is null || Manual || !OnLoad.HasDelegate || _loadStatus == InfiniteScrollLoadStatus.Empty)
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
            _parentSelector,
            Threshold.ToDouble());
        if (!exceeded)
        {
            return;
        }

        await DoLoadMore();
        StateHasChanged();
    }

    private async Task DoLoadMore()
    {
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
        if (_parentSelector is null)
        {
            return;
        }

        await Js.RemoveHtmlElementEventListener(_parentSelector, "scroll");
    }
}