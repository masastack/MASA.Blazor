namespace Masa.Blazor;

public interface IPopupService
{
    /// <summary>
    /// Clear all opened popup components.
    /// </summary>
    void Clear();

    /// <summary>
    /// Close the specified type of popup component.
    /// </summary>
    /// <param name="componentType"></param>
    void Close(Type componentType);

    /// <summary>
    /// Enqueue a snackbar to show.
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns
    Task EnqueueSnackbarAsync(SnackbarOptions options);

    /// <summary>
    /// Open the specified type of popup component and wait for the result.
    /// </summary>
    /// <param name="componentType"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    Task<object?> OpenAsync(Type componentType, IDictionary<string, object?> parameters);

    /// <summary>
    /// Open the specified type of popup component.
    /// </summary>
    /// <param name="componentType"></param>
    /// <param name="parameters"></param>
    void Open(Type componentType, IDictionary<string, object?>? parameters = null);

    /// <summary>
    /// Open or update the specified type of popup component with the given parameters.
    /// </summary>
    /// <param name="componentType"></param>
    /// <param name="parameters"></param>
    void OpenOrUpdate(Type componentType, IDictionary<string, object?> parameters);
}