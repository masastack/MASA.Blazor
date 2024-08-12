namespace Masa.Blazor.Popup;

public class ProviderItem(Type componentType, IDictionary<string, object?>? parameters, PopupService service)
{
    public Type ComponentType { get; set; } = componentType;

    public PopupService Service { get; set; } = service;

    public TaskCompletionSource<object?> TaskCompletionSource { get; set; } = new();

    public IDictionary<string, object?>? Parameters { get; set; } = parameters;

    public void Discard(object? result)
    {
        TaskCompletionSource.TrySetResult(result);
        Service.Remove(this);
    }
}
