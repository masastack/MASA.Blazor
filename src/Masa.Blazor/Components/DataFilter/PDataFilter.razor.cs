namespace Masa.Blazor.Presets;

public partial class PDataFilter : BDomComponentBase, IAsyncDisposable
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;

    [Parameter] public RenderFragment? ActionsContent { get; set; }

    [Parameter] [MassApiParameter(true)] public bool Dense { get; set; } = true;

    [Parameter] public bool HideResetButton { get; set; }

    [Parameter] public bool HideSearchButton { get; set; }

    [Parameter] [MassApiParameter(true)] public StringBoolean? HideDetails { get; set; } = true;

    [Parameter] public RenderFragment? HighFrequencyContent { get; set; }

    [Parameter] public RenderFragment? LowFrequencyContent { get; set; }

    [Parameter] public EventCallback OnSearch { get; set; }

    private bool _expanded;
    private bool _searching;
    private bool _resetting;
    private bool _firstRender = true;
    private StringNumber _height = 0;
    private bool _expanding = true;

    private ElementReference _lowFrequencyRef;

    private MInputsFilter? _inputsFilter;
    private IJSObjectReference? _jsModule;
    private bool _isBooted;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _firstRender = false;
        }
    }

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider.UseBem("m-data-filter")
                   .Element("high-frequency")
                   .Element("high-frequency-inputs", css => { css.AddIf("hidden", () => _expanded); })
                   .Element("high-frequency-actions")
                   .Element("low-frequency", css => { css.AddIf("expanded", () => _expanded); }, style =>
                   {
                       style.AddIf("display: none;", () => !_expanded && _firstRender)
                            .AddIf("opacity: 0;", () => !_expanded)
                            .AddIf("overflow: hidden", () => _expanding)
                            .AddHeight(_height);
                   });
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

            StateHasChanged();
        }
        else
        {
            _isBooted = true;

            NextTick(async () =>
            {
                await Task.Delay(16);

                if (_lowFrequencyRef.TryGetSelector(out var selector))
                {
                    _ = Js.AddHtmlElementEventListener<TransitionEventArgs>(selector, "transitionend", OnTransition, false);

                    await ShowLowFrequency();
                }
                else
                {
                    Logger.Log(LogLevel.Warning, "Failed to get LowFrequency's element reference");
                }
            });

            StateHasChanged();
        }
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
            await OnSearch.InvokeAsync();
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

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        try
        {
            if (_lowFrequencyRef.TryGetSelector(out var selector))
            {
                await Js.RemoveHtmlElementEventListener(selector, "transitionend");
            }
        }
        catch (Exception)
        {
            // ignored
        }
    }

    private class TransitionEventArgs
    {
        public string PropertyName { get; set; } = null!;
    }
}
