using Masa.Blazor.Presets;

namespace Masa.Blazor.Popup.Components;

public partial class EnqueuedSnackbars : BComponentBase
{
    [Inject]
    private IPopupService? PopupService { get; set; }

    [Inject]
    private MasaBlazor? MasaBlazor { get; set; }

    [Parameter]
    public SnackPosition Position { get; set; } = PEnqueuedSnackbars.DEFAULT_SNACK_POSITION;

    [Parameter]
    public int MaxCount { get; set; } = PEnqueuedSnackbars.DEFAULT_MAX_COUNT;

    [Parameter]
    public StringNumber MaxWidth { get; set; } = PEnqueuedSnackbars.DEFAULT_MAX_WIDTH;

    [Parameter]
    public int? Timeout { get; set; }

    [Parameter]
    public bool? Closeable { get; set; }
    
    [Parameter]
    public StringNumber? Elevation { get; set; }

    [Parameter]
    public bool Outlined { get; set; }

    [Parameter]
    public StringBoolean? Rounded { get; set; }

    [Parameter]
    public bool Shaped { get; set; }

    [Parameter]
    public bool Text { get; set; }

    private PEnqueuedSnackbars? _enqueuedSnackbars;

    protected override string ComponentName => PopupComponents.SNACKBAR;

    protected override async Task OnInitializedAsync()
    {
        if (PopupService is not null)
        {
            PopupService.SnackbarOpen += OnSnackbarOpenAsync;
        }

        await base.OnInitializedAsync();
    }

    private async Task OnSnackbarOpenAsync(SnackbarOptions config)
    {
        _enqueuedSnackbars?.EnqueueSnackbar(config);

        await Task.CompletedTask;
    }

    protected override void Dispose(bool disposing)
    {
        _enqueuedSnackbars?.Dispose();

        if (PopupService is not null)
        {
            PopupService.SnackbarOpen -= OnSnackbarOpenAsync;
        }

        base.Dispose(disposing);
    }
}
