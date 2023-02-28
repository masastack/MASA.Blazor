using Masa.Blazor.Presets;
using Masa.Blazor.Popup;

namespace Masa.Blazor;

public interface IPopupService
{
    Task<object> OpenAsync(Type componentType, IDictionary<string, object> parameters);

    #region Confirm

    Task<bool> ConfirmAsync(string title, string content);

    Task<bool> ConfirmAsync(string title, string content, AlertTypes type);

    Task<bool> ConfirmAsync(string title, string content, AlertTypes type, Func<PopupOkEventArgs, Task> onOk);

    Task<bool> ConfirmAsync(string title, string content, Func<PopupOkEventArgs, Task> onOk);

    Task<bool> ConfirmAsync(Action<ConfirmOptions> parameters);

    #endregion

    #region Prompt

    Task<string> PromptAsync(string title, string content);

    Task<string> PromptAsync(string title, string content, Func<PopupOkEventArgs<string?>, Task> onOk);

    Task<string> PromptAsync(Action<PromptOptions> parameters);

    #endregion

    #region Snackbar

    event Func<SnackbarOptions, Task> SnackbarOpen;

    Task EnqueueSnackbarAsync(string content, AlertTypes type = AlertTypes.None, bool closeable = false, int timeout = 5000);

    Task EnqueueSnackbarAsync(string title, string content, AlertTypes type = AlertTypes.None, bool closeable = false, int timeout = 5000);

    Task EnqueueSnackbarAsync(Exception exception, bool withStackTrace, bool closeable = false, int timeout = 5000);

    Task EnqueueSnackbarAsync(SnackbarOptions options);

    #endregion
}
