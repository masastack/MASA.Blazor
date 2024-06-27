namespace Masa.Blazor.Popup.Components;

public partial class EnqueuedSnackbars : ComponentBase, IAsyncDisposable
{
    [Inject]
    private IPopupService PopupService { get; set; } = null!;

    [Inject]
    private MasaBlazor MasaBlazor { get; set; } = null!;

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

    protected override async Task OnInitializedAsync()
    {
        PopupService.SnackbarOpen += OnSnackbarOpenAsync;
        MasaBlazor.DefaultsChanged += OnDefaultsChanged;

        await base.OnInitializedAsync();
    }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        if (MasaBlazor.Defaults is not null
            && MasaBlazor.Defaults.TryGetValue(PopupComponents.SNACKBAR, out var dictionary)
            && dictionary is not null)
        {
            var defaults = ParameterView.FromDictionary(dictionary);
            defaults.SetParameterProperties(this);
        }

        await base.SetParametersAsync(parameters);
    }

    private async void OnDefaultsChanged(object? sender, EventArgs e)
    {
        await SetParametersAsync(ParameterView.Empty); // it's ok?
    }

    private async Task OnSnackbarOpenAsync(SnackbarOptions config)
    {
        if (_enqueuedSnackbars is null)
        {
            return;
        }

        await _enqueuedSnackbars.EnqueueSnackbar(config);
    }

    public async ValueTask DisposeAsync()
    {
        if (_enqueuedSnackbars != null)
        {
            await _enqueuedSnackbars.DisposeAsync();
        }

        PopupService.SnackbarOpen -= OnSnackbarOpenAsync;
        MasaBlazor.DefaultsChanged -= OnDefaultsChanged;
    }
}