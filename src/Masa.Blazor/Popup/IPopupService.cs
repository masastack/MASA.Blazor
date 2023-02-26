using Masa.Blazor.Popup.Components;
using Masa.Blazor.Presets;

namespace Masa.Blazor;

public interface IPopupService
{
    Task<object> OpenAsync(Type componentType, Dictionary<string, object> parameters);

    #region Confirm

    Task<bool> ConfirmAsync(string title, string content);

    Task<bool> ConfirmAsync(string title, string content, AlertTypes type);

    Task<bool> ConfirmAsync(string title, string content, AlertTypes type, Func<PopupOkEventArgs, Task> onOk);

    Task<bool> ConfirmAsync(string title, string content, Func<PopupOkEventArgs, Task> onOk);

    Task<bool> ConfirmAsync(Action<ConfirmParameters> parameters);

    #endregion

    #region Prompt

    Task<string> PromptAsync(string title, string content);

    Task<string> PromptAsync(string title, string content, Func<PopupOkEventArgs<string?>, Task> onOk);

    Task<string> PromptAsync(Action<PromptParameters> parameters);

    #endregion

    #region Snackbar

    event Func<SnackbarOptions, Task> OnSnackbarOpen;

    Task EnqueueSnackbarAsync(string content, AlertTypes type = AlertTypes.None, bool closeable = false, int timeout = 5000);

    Task EnqueueSnackbarAsync(string title, string content, AlertTypes type = AlertTypes.None, bool closeable = false, int timeout = 5000);

    Task EnqueueSnackbarAsync(SnackbarOptions options);

    #endregion
}
