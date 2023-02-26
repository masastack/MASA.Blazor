using Masa.Blazor.Presets;

namespace Masa.Blazor.Popup.Components;

public partial class EnqueuedSnackbars : IDisposable
{
    [Inject]
    private IPopupService PopupService { get; set; }

    private PEnqueuedSnackbars _enqueuedSnackbars;

    private int _timeout = 5000;

    private int _maxCount = 5;

    private SnackPosition _position = SnackPosition.BottomCenter;

    protected override Task OnInitializedAsync()
    {
        if (PopupService != null)
        {
            PopupService.OnSnackbarOpen += NotifyAsync;
        }

        return base.OnInitializedAsync();
    }

    private Task NotifyAsync(SnackbarOptions config)
    {
        _enqueuedSnackbars.EnqueueSnackbar(config);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _enqueuedSnackbars?.Dispose();
        PopupService.OnSnackbarOpen -= NotifyAsync;
    }
}
