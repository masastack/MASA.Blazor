namespace Masa.Blazor.Popup.Components;

public class PopupComponentBase : BComponentBase
{
    [Inject]
    protected I18n I18n { get; set; } = null!;

    [CascadingParameter]
    protected ProviderItem? PopupItem { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? Attributes { get; set; }

    protected bool Visible { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        NextTick(() =>
        {
            Visible = true;
            StateHasChanged();
        });
    }
}
