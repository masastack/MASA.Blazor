#nullable enable
namespace Masa.Blazor.Popup.Components;

public class PopupComponentBase : BComponentBase
{
    [CascadingParameter]
    protected MApp? MApp { get; set; }

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
            return Task.CompletedTask;
        });
    }
}