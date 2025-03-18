namespace Masa.Blazor.Presets;

public partial class PDataFilter : MasaComponentBase
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;

    [Parameter] public RenderFragment? ActionsContent { get; set; }

    [Parameter] [MasaApiParameter(true)] public bool Dense { get; set; } = true;

    [Parameter] public bool HideResetButton { get; set; }

    [Parameter] public bool HideSearchButton { get; set; }

    [Parameter] [MasaApiParameter(true)] public StringBoolean? HideDetails { get; set; } = true;

    [Parameter] public RenderFragment? HighFrequencyContent { get; set; }

    [Parameter] public RenderFragment? LowFrequencyContent { get; set; }

    [Parameter] public EventCallback OnSearch { get; set; }

    /// <summary>
    /// Reset all the input fields and trigger the <see cref="OnSearch"/> event
    /// if this event is not set. 
    /// </summary>
    [Parameter]
    public EventCallback OnReset { get; set; }

    /// <summary>
    /// Expand the low frequency content when the component is initialized.
    /// </summary>
    [Parameter]
    public bool ExpandFirst { get; set; }

    private bool _expanded;
    private bool _searching;
    private bool _resetting;
    private bool _firstRender = true;
    private StringNumber _height = 0;
    private bool _expanding = true;

    private ElementReference _lowFrequencyRef;

    private MInputsFilter? _inputsFilter;
    private IJSObjectReference? _jsModule;
    private long _transitionendEventId;
    private bool _isBooted;

    private bool ComputedExpanded
    {
        get
        {
            if (_firstRender && ExpandFirst)
            {
                return true;
            }

            return _expanded;
        }
    }

    private bool IsBooted => ExpandFirst || _isBooted;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _firstRender = false;

            if (ExpandFirst)
            {
                _expanded = true;
                var scrollHeight = await GetProp<double>("scrollHeight");
                _height = scrollHeight == 0 ? "auto" : scrollHeight;
                StateHasChanged();
            }
        }
    }

    private static Block _block = new("m-data-filter");

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _block.Name;
    }

    private async Task ShowLowFrequency()
    {
        _expanding = true;

        if (_isBooted)
        {
            _expanded = true;

            var scrollHeight = await GetProp<double>("scrollHeight");

            _height = 0;

            StateHasChanged();

            _height = scrollHeight == 0 ? "auto" : scrollHeight;
        }
        else
        {
            _isBooted = true;

            NextTick(async () =>
            {
                await Task.Delay(16);

                if (_lowFrequencyRef.TryGetSelector(out var selector))
                {
                    _transitionendEventId = await Js.AddHtmlElementEventListener<TransitionEventArgs>(selector, "transitionend", OnTransition, false);

                    await ShowLowFrequency();
                }
                else
                {
                    Logger.Log(LogLevel.Warning, "Failed to get LowFrequency's element reference");
                }
            });
        }

        StateHasChanged();
    }

    private async Task OnTransition(TransitionEventArgs e)
    {
        if (!_expanded || e.PropertyName != "height")
        {
            return;
        }

        _expanding = false;

        _height = "auto";

        await InvokeStateHasChangedAsync();
    }

    private async Task HideLowFrequency()
    {
        _expanding = true;

        _expanded = false;

        _height = await GetProp<double>("clientHeight");
        StateHasChanged();

        await Task.Delay(10);

        _height = 0;

        StateHasChanged();
    }

    private async Task OnFieldChanged()
    {
        try
        {
            await OnSearch.InvokeAsync();
        }
        finally
        {
            _searching = false;
        }
    }

    private async Task HandleOnSearch()
    {
        _searching = true;
        StateHasChanged();

        await OnFieldChanged();
    }

    private async Task HandleOnClear()
    {
        _resetting = true;
        StateHasChanged();

        _inputsFilter!.ResetInputs();

        try
        {
            if (OnReset.HasDelegate)
            {
                await OnReset.InvokeAsync();
            }
            else
            {
                await OnSearch.InvokeAsync();
            }
        }
        finally
        {
            _resetting = false;
        }
    }

    private async Task<T> GetProp<T>(string identifier)
    {
        return await Js.InvokeAsync<T>(JsInteropConstants.GetProp, _lowFrequencyRef, identifier);
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        await Js.RemoveHtmlElementEventListener(_transitionendEventId);
    }

    private class TransitionEventArgs
    {
        public string PropertyName { get; set; } = null!;
    }
}