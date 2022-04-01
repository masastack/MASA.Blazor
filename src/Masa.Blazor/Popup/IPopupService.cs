using Masa.Blazor.Popup.Components;
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

    Task AlertAsync(string content);

    Task AlertAsync(string content, AlertTypes type);

    Task AlertAsync(Exception ex);

    Task AlertAsync(Action<AlertParameters> parameters);

    #endregion

    #region Toast
    event Action<ToastGlobalConfig> OnConfig;
    event Func<ToastConfig, Task> OnOpening;
    Task Config(ToastGlobalConfig config);
    Task Config(Action<ToastGlobalConfig> configAcion);
    Task ToastAsync(AlertTypes type, string title);
    Task ToastAsync(AlertTypes type, ToastConfig config);
    Task ToastAsync(AlertTypes type, Action<ToastConfig> configAction);
    Task ToastSuccessAsync(string title);
    Task ToastSuccessAsync(ToastConfig config);
    Task ToastSuccessAsync(Action<ToastConfig> configAction);
    Task ToastErrorAsync(string title);
    Task ToastErrorAsync(ToastConfig config);
    Task ToastErrorAsync(Action<ToastConfig> configAction);
    Task ToastInfoAsync(string title);
    Task ToastInfoAsync(ToastConfig config);
    Task ToastInfoAsync(Action<ToastConfig> configAction);
    Task ToastWarningAsync(string title);
    Task ToastWarningAsync(ToastConfig config);
    Task ToastWarningAsync(Action<ToastConfig> configAction);
    #endregion
}