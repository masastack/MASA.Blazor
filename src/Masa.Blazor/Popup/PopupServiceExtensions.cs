using Masa.Blazor.Popup;
using Masa.Blazor.Popup.Components;

namespace Masa.Blazor;

public static class PopupServiceExtensions
{
    #region EnqueueSnackbar

    public static async Task EnqueueSnackbarAsync(this IPopupService service, string content, AlertTypes type = AlertTypes.None,
        bool closeable = false, int timeout = 5000)
    {
        await service.EnqueueSnackbarAsync(new SnackbarOptions(content, type, closeable, timeout));
    }

    public static async Task EnqueueSnackbarAsync(this IPopupService service, string title, string content, AlertTypes type = AlertTypes.None,
        bool closeable = false,
        int timeout = 5000)
    {
        await service.EnqueueSnackbarAsync(new SnackbarOptions(title, content, type, closeable, timeout));
    }

    public static async Task EnqueueSnackbarAsync(this IPopupService service, Exception exception, bool withStackTrace = false,
        bool closeable = false, int timeout = 5000)
    {
        if (withStackTrace)
        {
            await service.EnqueueSnackbarAsync(exception.Message, exception.StackTrace ?? string.Empty, AlertTypes.Error, closeable, timeout);
        }
        else
        {
            await service.EnqueueSnackbarAsync(exception.Message, AlertTypes.Error, closeable, timeout);
        }
    }

    #endregion

    #region Confirm

    public static Task<bool> ConfirmAsync(this IPopupService service, string title, string content)
    {
        return service.ConfirmAsync(param =>
        {
            param.Title = title;
            param.Content = content;
        });
    }

    public static Task<bool> ConfirmAsync(this IPopupService service, string title, string content, AlertTypes type)
    {
        return service.ConfirmAsync(param =>
        {
            param.Title = title;
            param.Content = content;
            param.Type = type;
        });
    }

    public static Task<bool> ConfirmAsync(this IPopupService service, string title, string content, Func<PopupOkEventArgs, Task> onOk)
    {
        return service.ConfirmAsync(param =>
        {
            param.Title = title;
            param.Content = content;
            param.OnOk = onOk;
        });
    }

    public static Task<bool> ConfirmAsync(this IPopupService service, string title, string content, AlertTypes type,
        Func<PopupOkEventArgs, Task> onOk)
    {
        return service.ConfirmAsync(param =>
        {
            param.Title = title;
            param.Content = content;
            param.Type = type;
            param.OnOk = onOk;
        });
    }

    public static async Task<bool> ConfirmAsync(this IPopupService service, Action<ConfirmOptions> parameters)
    {
        ConfirmOptions param = new();

        parameters.Invoke(param);

        var res = await service.OpenAsync(typeof(Confirm), param.ToParameters());

        if (res is bool value)
        {
            return value;
        }

        return false;
    }

    #endregion

    #region Prompt

    public static Task<string?> PromptAsync(this IPopupService service, string title, string content)
    {
        return service.PromptAsync(param =>
        {
            param.Title = title;
            param.Content = content;
        });
    }

    /// <summary>
    /// Open a prompt dialog.
    /// </summary>
    /// <param name="service">The popup service</param>
    /// <param name="title">The prompt title</param>
    /// <param name="content">The prompt content</param>
    /// <param name="rule">The rule to validate the input value</param>
    /// <returns>The input value</returns>
    public static Task<string?> PromptAsync(this IPopupService service, string title, string content, Func<string, StringBoolean> rule)
    {
        return service.PromptAsync(param =>
        {
            param.Title = title;
            param.Content = content;
            param.Rule = rule;
        });
    }

    public static Task<string?> PromptAsync(this IPopupService service, string title, string content, Func<PopupOkEventArgs<string?>, Task> onOk)
    {
        return service.PromptAsync(param =>
        {
            param.Title = title;
            param.Content = content;
            param.OnOk = onOk;
        });
    }

    /// <summary>
    /// Open a prompt dialog.
    /// </summary>
    /// <param name="service">The popup service</param>
    /// <param name="title">The prompt title</param>
    /// <param name="content">The prompt content</param>
    /// <param name="rule">The rule to validate the input value</param>
    /// <param name="onOk">The callback when the ok button was clicked</param>
    /// <returns>The input value</returns>
    public static Task<string?> PromptAsync(this IPopupService service, string title, string content, Func<string, StringBoolean> rule,
        Func<PopupOkEventArgs<string?>, Task> onOk)
    {
        return service.PromptAsync(param =>
        {
            param.Title = title;
            param.Content = content;
            param.Rule = rule;
            param.OnOk = onOk;
        });
    }

    public static async Task<string?> PromptAsync(this IPopupService service, Action<PromptOptions> parameters)
    {
        PromptOptions param = new();

        parameters.Invoke(param);

        var res = await service.OpenAsync(typeof(Prompt), param.ToParameters());

        return (string?)res;
    }

    #endregion

    #region Progress

    public static void ShowProgressCircular(this IPopupService service, Action<ProgressCircularOptions>? options = null)
    {
        ProgressCircularOptions param = new();
        options?.Invoke(param);

        service.Open(typeof(ProgressCircular), param.ToParameters());
    }

    public static void HideProgressCircular(this IPopupService service)
    {
        service.Close(typeof(ProgressCircular));
    }

    public static void ShowProgressLinear(this IPopupService service, Action<ProgressLinearOptions>? options = null)
    {
        ProgressLinearOptions param = new();
        options?.Invoke(param);

        service.Open(typeof(ProgressLinear), param.ToParameters());
    }

    public static void HideProgressLinear(this IPopupService service)
    {
        service.Close(typeof(ProgressLinear));
    }

    #endregion
}
