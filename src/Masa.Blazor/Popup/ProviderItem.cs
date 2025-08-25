namespace Masa.Blazor.Popup;

public class ProviderItem(Type componentType, IDictionary<string, object?>? parameters, PopupService service)
{
    public Type ComponentType { get; set; } = componentType;

    public PopupService Service { get; set; } = service;

    public TaskCompletionSource<object?> TaskCompletionSource { get; set; } = new();

    public IDictionary<string, object?>? Parameters { get; set; } = parameters;

    internal event EventHandler? OnUpdate;

    public void Discard(object? result)
    {
        TaskCompletionSource.TrySetResult(result);
        Service.Remove(this);
    }

    public void UpdateParameters(IDictionary<string, object?> newParameters)
    {
        Parameters ??= new Dictionary<string, object?>();

        foreach (var key in Parameters.Keys)
        {
            if (!newParameters.ContainsKey(key))
                Parameters[key] = null;
        }

        foreach (var (key, value) in newParameters)
        {
            Parameters[key] = value;
        }

        // Update the refresh token to trigger the lifecycle methods in the components
        Parameters["RefreshToken"] = Guid.NewGuid();

        OnUpdate?.Invoke(this, EventArgs.Empty);
    }
}