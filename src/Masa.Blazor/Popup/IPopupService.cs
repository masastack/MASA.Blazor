namespace Masa.Blazor;

public interface IPopupService
{
    Task<object?> OpenAsync(Type componentType, IDictionary<string, object?> parameters);

    void Open(Type componentType, IDictionary<string, object?>? parameters = null);

    void Close(Type componentType);

    #region Snackbar

    event Func<SnackbarOptions, Task> SnackbarOpen;
    Task EnqueueSnackbarAsync(SnackbarOptions options);

    #endregion
}
