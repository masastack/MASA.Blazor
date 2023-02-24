using Masa.Blazor.Popup.Components;
using Masa.Blazor.Presets;
using Masa.Blazor.Presets.EnqueuedSnackbars;
using OneOf;

namespace Masa.Blazor;

public interface IPopupService
{
    #region Confirm

    Task<bool> ConfirmAsync(string title, string content);

    Task<bool> ConfirmAsync(string title, string content, AlertTypes type);

    Task<bool> ConfirmAsync(string title, string content, AlertTypes type, Func<PopupOkEventArgs, Task> onOk);

    Task<bool> ConfirmAsync(string title, string content, Func<PopupOkEventArgs, Task> onOk);

    Task<bool> ConfirmAsync(Action<ConfirmParameters> parameters);

    #endregion

    Task<object> OpenAsync(Type componentType, Dictionary<string, object> parameters);

    #region Prompt

    Task<string> PromptAsync(string title, string content);

    Task<string> PromptAsync(string title, string content, Func<PopupOkEventArgs<string?>, Task> onOk);

    Task<string> PromptAsync(Action<PromptParameters> parameters);

    #endregion

    #region Alert

    Task ShowSnackbarAsync(string content);

    Task ShowSnackbarAsync(string content, AlertTypes type);

    Task ShowSnackbarAsync(Exception ex);

    Task ShowSnackbarAsync(Action<SnackbarParameters> parameters);

    #endregion

    #region Toast
    event Action<ToastGlobalConfig> OnToastConfig;
    event Func<ToastConfig, Task> OnToastOpening;
    Task ConfigToast(ToastGlobalConfig config);
    Task ConfigToast(Action<ToastGlobalConfig> configAcion);
    Task ToastAsync(string title, AlertTypes type);
    Task ToastAsync(ToastConfig config);
    Task ToastAsync(Action<ToastConfig> configAction);
    Task ToastSuccessAsync(string title);
    Task ToastErrorAsync(string title);
    Task ToastInfoAsync(string title);
    Task ToastWarningAsync(string title);
    #endregion
}