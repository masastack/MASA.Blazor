using Masa.Blazor.Popup.Components;

namespace Masa.Blazor;

public partial class PopupService : IPopupService
{
    private readonly IPopupProvider _popupProvider;

    private bool _enqueuedSnackbarsRendered;

    public PopupService(IPopupProvider popupProvider)
    {
        Console.WriteLine("PopupService register");
        
        _popupProvider = popupProvider;

        OpenAsync(typeof(EnqueuedSnackbars), new Dictionary<string, object>());
    }

    public Task<object> OpenAsync(Type componentType, Dictionary<string, object> parameters)
    {
        var item = _popupProvider.Add(componentType, parameters, this, nameof(PopupService));
        return item.TaskCompletionSource.Task;
    }
}