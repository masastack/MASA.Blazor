#nullable enable

namespace Masa.Blazor.Presets.EnqueuedSnackbars;

public partial class Snackbar
{
    [Inject] private I18n I18n { get; set; } = null!;

    [CascadingParameter] private ProviderItem? PopupItem { get; set; }

    [CascadingParameter] private PEnqueuedSnackbars? Toast { get; set; }

    #region parameters from MSnackbar

    [Parameter] public bool Bottom { get; set; }

    [Parameter] public bool Centered { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] public string? ContentClass { get; set; }

    [Parameter] public bool Dark { get; set; }

    [Parameter] public StringNumber? Elevation { get; set; }

    [Parameter] public StringNumber? Height { get; set; }

    [Parameter] public bool Left { get; set; }

    [Parameter] public bool Light { get; set; }

    [Parameter] public bool MultiLine { get; set; }

    [Parameter] public EventCallback OnClosed { get; set; }

    [Parameter] public bool Outlined { get; set; }

    [Parameter] public bool Right { get; set; }

    [Parameter] public StringBoolean? Rounded { get; set; }

    [Parameter] public bool Shaped { get; set; }

    [Parameter] public bool Text { get; set; }

    [Parameter] public bool Tile { get; set; }

    [Parameter] public int Timeout { get; set; } = 5000;

    [Parameter] public bool Top { get; set; }

    [Parameter] public bool Vertical { get; set; }

    [Parameter] public StringNumber? Width { get; set; }

    #endregion

    [Parameter] public Guid EnqueueId { get; set; }

    [Parameter] public string? Title { get; set; }

    [Parameter, EditorRequired] public string? Content { get; set; }

    [Parameter] public AlertTypes Type { get; set; }

    [Parameter] public Action<ModalButtonProps>? ActionProps { get; set; }

    [Parameter] public string? ActionText { get; set; }

    [Parameter] public Func<Task>? OnAction { get; set; }

    [Parameter] public bool Closeable { get; set; }

    private bool _visible = true;
    
    private ModalButtonProps? ComputedActionButtonProps { get; set; }

    protected string? ComputedColor
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(Color))
            {
                return Color;
            }

            return Type switch
            {
                AlertTypes.Success => "success",
                AlertTypes.Info => "info",
                AlertTypes.Warning => "warning",
                AlertTypes.Error => "error",
                _ => null
            };
        }
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ActionText ??= I18n.T("$masaBlazor.close");

        ComputedActionButtonProps = new ModalButtonProps()
        {
            Text = true
        };

        ActionProps?.Invoke(ComputedActionButtonProps);
    }

    private async Task HandleOnClosed()
    {
        if (OnClosed.HasDelegate)
        {
            await OnClosed.InvokeAsync();
        }

        DiscardPopupItem();
    }

    private void HandleOnClose()
    {
        Toast?.RemoveNoRender(EnqueueId);
    }

    private async Task HandleOnAction()
    {
        if (OnAction is not null)
        {
            await OnAction.Invoke();
        }

        DiscardPopupItem();
    }

    private void DiscardPopupItem()
    {
        if (Toast is not null) return;

        PopupItem?.Discard(true);
    }
}
