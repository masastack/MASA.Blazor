using Masa.Blazor.Popup.Components;

namespace Masa.Blazor;

public class PopupService : IPopupService
{
    private readonly IPopupProvider _popupProvider;

    public event Func<SnackbarOptions, Task>? SnackbarOpen;

    public PopupService(IPopupProvider popupProvider)
    {
        _popupProvider = popupProvider;

        _ = OpenAsync(typeof(EnqueuedSnackbars), new Dictionary<string, object?>());
    }

    public void Open(Type componentType, IDictionary<string, object?>? parameters = null)
    {
        OpenComponent(componentType, parameters);
    }

    public Task<object?> OpenAsync(Type componentType, IDictionary<string, object?> parameters)
    {
        return OpenComponent(componentType, parameters).TaskCompletionSource.Task;
    }

    public void Close(Type componentType)
    {
        var item = _popupProvider.GetItems().LastOrDefault(u => u.ComponentType == componentType);
        if (item is not null)
        {
            _popupProvider.Remove(item);
        }
    }

    public async Task EnqueueSnackbarAsync(SnackbarOptions options)
    {
        if (SnackbarOpen is null) return;

        await SnackbarOpen.Invoke(options);
    }

    private ProviderItem OpenComponent(Type componentType, IDictionary<string, object?>? parameters)
    {
        return _popupProvider.Add(componentType, parameters, this, nameof(PopupService));
    }
}
