namespace Masa.Blazor.Popup.Components;

public partial class Toast : PopupComponentBase
{
    [Parameter, EditorRequired]
    public string? Content { get; set; }

    [Parameter]
    public string? Icon { get; set; }

    [Parameter]
    public bool DisableOutsideClick { get; set; }

    [Parameter]
    public ToastType Type { get; set; }

    [Parameter]
    [MasaApiParameter("$success")]
    public string? SuccessIcon { get; set; } = "$success";

    [Parameter]
    [MasaApiParameter("$error")]
    public string? ErrorIcon { get; set; } = "$error";

    [Parameter]
    [MasaApiParameter(2000)]
    public int Duration { get; set; } = 2000;

    /// <summary>
    /// Internal use only.
    /// </summary>
    [Parameter]
    public Guid RefreshToken { get; set; }

    public const string ComponentId = "PopupService.Toast";

    private static Block _block = new Block("m-toast");
    private static Element _contentElement = _block.Element("content");
    private static Element _messageElement = _block.Element("message");
    private ModifierBuilder _contentModifier = _contentElement.CreateModifierBuilder();
    private CancellationTokenSource _cancellationTokenSource = new();

    private bool _resetTimerOnUpdate;

    private bool HideOverlay => Type != ToastType.Loading && !DisableOutsideClick;

    protected override string ComponentName { get; } = ComponentId;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (Type != ToastType.Loading)
        {
            _ = DelayedCloseAsync(Duration, _cancellationTokenSource.Token);
        }
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Type == ToastType.Loading)
        {
            Content ??= I18n.T("$masaBlazor.pullRefresh.loadingText");
        }

        if (_resetTimerOnUpdate)
        {
            _resetTimerOnUpdate = false;

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new();

            if (Type != ToastType.Loading)
            {
                _ = DelayedCloseAsync(Duration, _cancellationTokenSource.Token);
            }
        }
    }

    protected override IEnumerable<string?> BuildComponentClass()
    {
        yield return _contentElement.Name;
    }

    private async Task DelayedCloseAsync(int millisecondsDelay, CancellationToken cancellationToken)
    {
        try
        {
            await Task.Delay(millisecondsDelay, cancellationToken);
            Visible = false;
            await InvokeAsync(StateHasChanged);
        }
        catch (TaskCanceledException)
        {
            // Ignore the exception if the task was canceled
        }
    }

    protected override void OnUpdate(object? sender, EventArgs e)
    {
        base.OnUpdate(sender, e);
        _resetTimerOnUpdate = true;
    }
}