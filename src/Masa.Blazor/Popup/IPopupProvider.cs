namespace Masa.Blazor.Popup;

public interface IPopupProvider
{
    event EventHandler StateChanged;

    ProviderItem Add(Type componentType, IDictionary<string, object?>? attributes, object service, string serviceName);

    void Remove(ProviderItem item);

    IEnumerable<ProviderItem> GetItems();
}
