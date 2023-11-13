namespace Masa.Blazor.Presets;

public partial class EllipsisText : IAsyncDisposable
{
    [Inject] private IJSRuntime Js { get; set; } = null!;

    [Parameter] public bool Bottom { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public bool Left { get; set; }

    [Parameter] public StringNumber? NudgeBottom { get; set; }

    [Parameter] public StringNumber? NudgeLeft { get; set; }

    [Parameter] public StringNumber? NudgeRight { get; set; }

    [Parameter] public StringNumber? NudgeTop { get; set; }

    [Parameter] public bool Right { get; set; }

    [Parameter] public string? Tooltip { get; set; }

    [Parameter] public string? TooltipClass { get; set; }

    [Parameter] public string? TooltipStyle { get; set; }

    [Parameter] public StringNumber? NudgeWidth { get; set; }

    [Parameter] public StringNumber? MaxWidth { get; set; } = "25%";

    [Parameter] public StringNumber? MaxHeight { get; set; } = "25%";

    [Parameter] public StringNumber? MinWidth { get; set; }

    [Parameter] public bool OffsetY { get; set; } = true;

    [Parameter] public bool OffsetX { get; set; }

    [Parameter] public bool Top { get; set; }

    [Parameter] public RenderFragment<ActivatorRefProps>? ActivatorContent { get; set; }

    private bool _isDisabled;

    private ElementReference _spanReference;

    private IJSObjectReference? _jsModule;

    private IJSObjectReference? _resizeObserverDisposable;

    private DotNetObjectReference<EllipsisText>? _selfReference;

    private ActivatorRefProps? _activatorRefProps;

    private bool _isDisposed;

    protected override void OnParametersSet()
    {
        if (!(Left || Right || Bottom))
        {
            Top = true;
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (_activatorRefProps is not null)
            {
                _spanReference = _activatorRefProps.Ref.Current;
            }

            _selfReference = DotNetObjectReference.Create(this);
            _jsModule = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/Masa.Blazor/Presets/EllipsisText/EllipsisText.razor.js");
            _resizeObserverDisposable =
                await _jsModule.InvokeAsync<IJSObjectReference>("observer", _spanReference, _selfReference);
        }
    }

    [JSInvokable]
    public void OnEllipsisChange(bool isEllipsis)
    {
        _isDisabled = !isEllipsis;
        if (!_isDisposed)
            StateHasChanged();
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        try
        {
            _isDisposed = true;

            await _resizeObserverDisposable.InvokeVoidAsync("disconnect");
            await _resizeObserverDisposable.DisposeAsync();
            await _jsModule.DisposeAsync();
            _selfReference?.Dispose();
        }
        catch
        {
            // ignored
        }
    }
}
