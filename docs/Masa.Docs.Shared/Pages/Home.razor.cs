namespace Masa.Docs.Shared.Pages;

public partial class Home : ComponentBase
{
    [CascadingParameter(Name = "Culture")]
    private string? Culture { get; set; }

    [Parameter]
    [SupplyParameterFromQuery]
    public string Product { get; set; } = "stack";

    private bool _prevXs;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        MasaBlazor.BreakpointChanged += MasaBlazorOnBreakpointChanged;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            _prevXs = MasaBlazor.Breakpoint.Xs;
        }
    }

    private void MasaBlazorOnBreakpointChanged(object? sender, BreakpointChangedEventArgs e)
    {
        if (_prevXs != MasaBlazor.Breakpoint.Xs)
        {
            _prevXs = MasaBlazor.Breakpoint.Xs;
            InvokeAsync(StateHasChanged);
        }
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Product ??= Culture == "zh-CN" ? "stack" : "blazor";
    }

    public void Dispose()
    {
        MasaBlazor.BreakpointChanged -= MasaBlazorOnBreakpointChanged;
    }
}
