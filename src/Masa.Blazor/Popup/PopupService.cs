namespace Masa.Blazor;

public partial class PopupService : IPopupService
{
    private readonly IPopupProvider _popupProvider;

    public PopupService(IPopupProvider popupProvider)
    {
        _popupProvider = popupProvider;
    }

    public Task<object> OpenAsync(Type componentType, Dictionary<string, object> parameters)
    {
        var item = _popupProvider.Add(componentType, parameters, this, nameof(PopupService));
        return item.TaskCompletionSource.Task;
    }
}