using Masa.Blazor.Popup.Components;
using System.Diagnostics.CodeAnalysis;

namespace Masa.Blazor;

[SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.")]
public partial class PopupService
{
    public Task<bool> ConfirmAsync(string title, string content)
    {
        return ConfirmAsync(param =>
        {
            param.Title = title;
            param.Content = content;
        });
    }

    public Task<bool> ConfirmAsync(string title, string content, AlertTypes type)
    {
        return ConfirmAsync(param =>
        {
            param.Title = title;
            param.Content = content;
            param.Type = type;
        });
    }

    public Task<bool> ConfirmAsync(string title, string content, AlertTypes type, Func<PopupOkEventArgs, Task> onOk)
    {
        return ConfirmAsync(param =>
        {
            param.Title = title;
            param.Content = content;
            param.Type = type;
            param.OnOk = onOk;
        });
    }

    public Task<bool> ConfirmAsync(string title, string content, Func<PopupOkEventArgs, Task> onOk)
    {
        return ConfirmAsync(param =>
        {
            param.Title = title;
            param.Content = content;
            param.OnOk = onOk;
        });
    }

    public async Task<bool> ConfirmAsync(Action<ConfirmParameters> parameters)
    {
        ConfirmParameters param = new();

        parameters.Invoke(param);

        var res = await OpenAsync(typeof(Confirm), param.ToDictionary());

        if (res is bool value)
        {
            return value;
        }

        return false;
    }
}