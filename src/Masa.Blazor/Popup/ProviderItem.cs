namespace Masa.Blazor.Popup;

public class ProviderItem
{
    public Type ComponentType { get; set; }

    public PopupProvider Provider { get; set; }

    public TaskCompletionSource<object?> TaskCompletionSource { get; set; }

    public object Service { get; set; }

    public string ServiceName { get; set; }

    public IDictionary<string, object?>? Parameters { get; set; }

    public ProviderItem(Type componentType, IDictionary<string, object?>? parameters, PopupProvider provider, object service, string serviceName)
    {
        TaskCompletionSource = new();
        Parameters = parameters;
        ComponentType = componentType;
        Provider = provider;
        Service = service;
        ServiceName = serviceName;
    }

    public void Discard(object? result)
    {
        TaskCompletionSource.TrySetResult(result);
        Provider.Remove(this);
    }
}
