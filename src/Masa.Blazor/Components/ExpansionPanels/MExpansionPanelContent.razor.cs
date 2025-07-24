namespace Masa.Blazor;

public partial class MExpansionPanelContent : MasaComponentBase
{
    [CascadingParameter] public MExpansionPanel? ExpansionPanel { get; set; }

    [Parameter] public bool Eager { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Re-render requests will also be responded to when inactive.
    /// By default, only active expansion panel content will be rendered.
    /// </summary>
    [Parameter]
    [MasaApiParameter(ReleasedIn = "v1.10.3")]
    public bool ShouldRenderWhenInactive { get; set; }

    private bool _isBooted;
    private bool _booting;
    private bool _isActive;

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (ExpansionPanel != null)
        {
            if ((_isBooted && _booting == false) || Eager)
            {
                _isActive = ExpansionPanel.InternalIsActive;
            }
            else if (ExpansionPanel.Booted)
            {
                _isBooted = true;
                _booting = true;

                if (ExpansionPanel.InternalIsActive)
                {
                    await Task.Delay(16);

                    _booting = false;
                    _isActive = true;
                }
            }
        }
    }

    private Block _block = new("m-expansion-panel-content");

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _block.Name;
    }
}