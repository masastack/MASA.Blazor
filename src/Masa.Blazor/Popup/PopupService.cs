using Masa.Blazor.Popup.Components;

namespace Masa.Blazor;

public partial class PopupService : IPopupService
{
    private readonly IPopupProvider _popupProvider;

    public PopupService(IPopupProvider popupProvider)
    {
        _popupProvider = popupProvider;

        _ = OpenAsync(typeof(EnqueuedSnackbars), new Dictionary<string, object>());
    }

    public Task<object> OpenAsync(Type componentType, IDictionary<string, object> parameters)
    {
        var item = _popupProvider.Add(componentType, parameters, this, nameof(PopupService));
        return item.TaskCompletionSource.Task;
    }
}
