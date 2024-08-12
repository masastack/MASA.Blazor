using Masa.Blazor.Popup;
using Masa.Blazor.Popup.Components;

namespace Masa.Blazor;

public class PopupService : IPopupService
{
    private readonly List<ProviderItem> _items = [];
    private readonly object _obj = new();

    internal event EventHandler? StateChanged;
    internal event Func<SnackbarOptions, Task>? SnackbarOpen;

    public PopupService()
    {
        _ = OpenAsync(typeof(EnqueuedSnackbars), new Dictionary<string, object?>());
    }

    public void Open(Type componentType, IDictionary<string, object?>? parameters = null)
    {
        Add(componentType, parameters);
    }

    public Task<object?> OpenAsync(Type componentType, IDictionary<string, object?> parameters)
    {
        return Add(componentType, parameters).TaskCompletionSource.Task;
    }

    public void Close(Type componentType)
    {
        var item = GetItems().LastOrDefault(u => u.ComponentType == componentType);
        if (item is not null)
        {
            Remove(item);
        }
    }

    public void Clear()
    {
        _items.Clear();
        StateHasChanged();
    }

    public async Task EnqueueSnackbarAsync(SnackbarOptions options)
    {
        if (SnackbarOpen is null)
            return;

        await SnackbarOpen.Invoke(options);
    }

    internal ProviderItem Add(Type componentType, IDictionary<string, object?>? attributes)
    {
        var item = new ProviderItem(componentType, attributes, this);

        lock (_obj)
        {
            _items.Add(item);

            StateHasChanged();

            return item;
        }
    }

    internal void Remove(ProviderItem item)
    {
        lock (_obj)
        {
            _items.Remove(item);

            StateHasChanged();
        }
    }

    internal IEnumerable<ProviderItem> GetItems()
    {
        lock (_obj)
        {
            return _items;
        }
    }

    private void StateHasChanged()
    {
        StateChanged?.Invoke(this, EventArgs.Empty);
    }
}
