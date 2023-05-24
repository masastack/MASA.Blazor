using Masa.Blazor.Popup.Components;

namespace Masa.Blazor;

public partial class PopupService : IPopupService
{
    private readonly IPopupProvider _popupProvider;

    public PopupService(IPopupProvider popupProvider)
    {
        _popupProvider = popupProvider;

        _ = OpenAsync(typeof(EnqueuedSnackbars), new Dictionary<string, object?>());
    }

    public void Open(Type componentType)
    {
        OpenCompoent(componentType);
    }

    public Task<object?> OpenAsync(Type componentType, IDictionary<string, object?> parameters)
    {
        return OpenCompoent(componentType, parameters).TaskCompletionSource.Task;
    }

    public void Close(Type componentType)
    {
        var item = _popupProvider.GetItems().FirstOrDefault(u => u.ComponentType == componentType);
        if (item is not null)
        {
            _popupProvider.Remove(item);
        }
    }

    private ProviderItem OpenCompoent(Type componentType, IDictionary<string, object?>? parameters = null)
    {
        return _popupProvider.Add(componentType, parameters, this, nameof(PopupService));
    }
}
