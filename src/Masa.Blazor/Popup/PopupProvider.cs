namespace Masa.Blazor.Popup;

public class PopupProvider : IPopupProvider
{
    private readonly List<ProviderItem> _items = new();
    private readonly object _obj = new();

    public event EventHandler? StateChanged;

    public ProviderItem Add(Type componentType, IDictionary<string, object?>? attributes, object service, string serviceName)
    {
        var item = new ProviderItem(componentType, attributes, this, service, serviceName);

        lock (_obj)
        {
            _items.Add(item);
        }

        StateHasChanged();

        return item;
    }

    public void Remove(ProviderItem item)
    {
        lock (_obj)
        {
            _items.Remove(item);
        }

        StateHasChanged();
    }

    public IEnumerable<ProviderItem> GetItems()
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
