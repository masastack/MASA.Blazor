namespace Masa.Blazor.Popup.Components;

public class PopupComponentBase : BComponentBase
{
    [Inject] protected I18n I18n { get; set; } = null!;

    [CascadingParameter] private ProviderItem? PopupItem { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? Attributes { get; set; }

    protected bool Visible { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        NextTick(async () =>
        {
            // ensure the transition animation could be invoked
            // TODO: do not work in Confirm and Prompt, come back after the Transition component is refactored.
            await Task.Delay(16);

            Visible = true;
            StateHasChanged();
        });
    }

    /// <summary>
    /// Close the opened popup component and return the value argument as feedback.
    /// </summary>
    /// <param name="returnVal">return value for the <see cref="IPopupService"/>'s Open method.</param>
    protected async Task ClosePopupAsync(object? returnVal = null)
    {
        if (PopupItem != null)
        {
            Visible = false;
            await Task.Delay(256);
            PopupItem.Discard(returnVal);
        }
    }
}
