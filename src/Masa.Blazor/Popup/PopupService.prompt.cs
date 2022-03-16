using Masa.Blazor.Popup.Components;

namespace Masa.Blazor;

public partial class PopupService
{
    public Task<string> PromptAsync(string title, string content)
    {
        return PromptAsync(param =>
        {
            param.Title = title;
            param.Content = content;
        });
    }

    public Task<string> PromptAsync(string title, string content, Func<PopupOkEventArgs<string?>, Task> onOk)
    {
        return PromptAsync(param =>
        {
            param.Title = title;
            param.Content = content;
            param.OnOk = onOk;
        });
    }

    public async Task<string> PromptAsync(Action<PromptParameters> parameters)
    {
        PromptParameters param = new();

        parameters.Invoke(param);

        var res = await OpenAsync(typeof(Prompt), param.ToDictionary());

        return (string)res;
    }
}