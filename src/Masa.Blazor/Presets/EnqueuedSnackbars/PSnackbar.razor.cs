#nullable enable

namespace Masa.Blazor.Presets.EnqueuedSnackbars;

public partial class PSnackbar
{
    [CascadingParameter] private ProviderItem? PopupItem { get; set; }

    [CascadingParameter] private PEnqueuedSnackbars? EnqueuedSnacks { get; set; }

    #region parameters from MSnackbar

    [Parameter] public string? Class { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] public string? ContentClass { get; set; }

    [Parameter] public bool Dark { get; set; }

    [Parameter] public StringNumber? Elevation { get; set; }

    [Parameter] public StringNumber? Height { get; set; }

    [Parameter] public bool Light { get; set; }

    [Parameter] public bool MultiLine { get; set; }

    [Parameter] public EventCallback OnClosed { get; set; }

    [Parameter] public bool Outlined { get; set; }

    [Parameter] public StringBoolean? Rounded { get; set; }

    [Parameter] public bool Shaped { get; set; }

    [Parameter] public string? Style { get; set; }

    [Parameter] public bool Text { get; set; }

    [Parameter] public bool Tile { get; set; }

    [Parameter] public int Timeout { get; set; } = 5000;

    [Parameter] public bool Vertical { get; set; }

    [Parameter] public StringNumber? Width { get; set; }

    #endregion

    [Parameter] public Guid EnqueueId { get; set; }

    [Parameter] public string? Title { get; set; }

    [Parameter, EditorRequired] public string? Content { get; set; }

    [Parameter] public AlertTypes Type { get; set; }

    [Parameter] public string? ActionColor { get; set; }

    [Parameter] public string? ActionText { get; set; }

    [Parameter] public EventCallback OnAction { get; set; }

    [Parameter] public bool Closeable { get; set; }

    private bool _visible = true;

    private string? ComputedColor
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

    private int ComputedTimeout
    {
        get
        {
            var timeout = EnqueuedSnacks?.Timeout ?? Timeout;
            return timeout >= 0 ? timeout : 0;
        }
    }

    private bool ComputedCloseable => EnqueuedSnacks?.Closeable ?? Closeable;

    private async Task HandleOnAction()
    {
        if (OnAction.HasDelegate)
        {
            await OnAction.InvokeAsync();
        }

        HandleOnClose();
    }

    private async Task HandleOnClosed()
    {
        if (OnClosed.HasDelegate)
        {
            await OnClosed.InvokeAsync();
        }

        EnqueuedSnacks?.RemoveNoRender(EnqueueId);

        DiscardPopupItem();
    }

    private void HandleOnClose()
    {
        _visible = false;
    }

    private void DiscardPopupItem()
    {
        if (EnqueuedSnacks is not null) return;

        PopupItem?.Discard(true);
    }
}
