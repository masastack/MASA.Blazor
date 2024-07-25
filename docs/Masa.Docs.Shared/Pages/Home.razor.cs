namespace Masa.Docs.Shared.Pages;

public partial class Home : ComponentBase
{
    [CascadingParameter(Name = "Culture")]
    private string? Culture { get; set; }

    [Parameter]
    [SupplyParameterFromQuery]
    public string Product { get; set; } = "stack";
    
    [SupplyParameterFromQuery(Name = "v")]
    [Parameter] public string? Version { get; set; }
    
    protected override void OnInitialized()
    {
        base.OnInitialized();

        MasaBlazor.BreakpointChanged += MasaBlazorOnBreakpointChanged;
    }

    private void MasaBlazorOnBreakpointChanged(object? sender, BreakpointChangedEventArgs e)
    {
        if (e.Breakpoint == Breakpoints.Xs)
        {
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
