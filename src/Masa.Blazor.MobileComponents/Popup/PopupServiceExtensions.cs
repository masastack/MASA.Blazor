using Masa.Blazor.Popup.Components;

namespace Masa.Blazor;

public static class PopupServiceExtensions
{
    /// <summary>
    /// Show a simple toast, you can customize the icon.
    /// If you want to show success or error toast, please use <see cref="ShowSuccessToast"/> or <see cref="ShowErrorToast"/>.
    /// The toast will auto close after a few seconds. (default 2 seconds)
    /// </summary>
    /// <param name="popupService"></param>
    /// <param name="content"></param>
    /// <param name="icon"></param>
    /// <param name="disableOutsideClick"></param>
    public static void ShowToast(this IPopupService popupService, string content, string? icon = null,
        bool disableOutsideClick = false)
    {
        ShowToastInternal(popupService, content, icon, ToastType.Default, disableOutsideClick);
    }

    /// <summary>
    /// Show success toast, icon is optional. The toast will auto close after a few seconds. (default 2 seconds)
    /// </summary>
    /// <param name="popupService"></param>
    /// <param name="content"></param>
    /// <param name="disableOutsideClick"></param>
    public static void ShowSuccessToast(this IPopupService popupService, string content,
        bool disableOutsideClick = false)
    {
        ShowToastInternal(popupService, content, null, ToastType.Success, disableOutsideClick);
    }

    /// <summary>
    /// Show error toast, icon is optional. The toast will auto close after a few seconds. (default 2 seconds)
    /// </summary>
    /// <param name="popupService"></param>
    /// <param name="content"></param>
    /// <param name="disableOutsideClick"></param>
    public static void ShowErrorToast(this IPopupService popupService, string content,
        bool disableOutsideClick = false)
    {
        ShowToastInternal(popupService, content, null, ToastType.Error, disableOutsideClick);
    }

    /// <summary>
    /// Show loading toast, content is optional. Invoke <see cref="CloseToast"/> to close the toast.
    /// </summary>
    /// <param name="popupService"></param>
    /// <param name="content"></param>
    /// <param name="allowOutsideClick"></param>
    public static void ShowLoadingToast(this IPopupService popupService, string? content = null,
        bool allowOutsideClick = true)
    {
        ShowToastInternal(popupService, content, null, ToastType.Loading, !allowOutsideClick);
    }

    /// <summary>
    /// Close the loading toast manually.
    /// </summary>
    /// <param name="popupService"></param>
    public static void CloseToast(this IPopupService popupService)
    {
        popupService.Close(typeof(Toast));
    }

    private static void ShowToastInternal(this IPopupService popupService, string? content, string? icon,
        ToastType type = ToastType.Default, bool disableOutsideClick = false)
    {
        var options = new ToastOptions(content, icon, type, disableOutsideClick);
        popupService.OpenOrUpdate(typeof(Toast), options.ToParameters());
    }
}