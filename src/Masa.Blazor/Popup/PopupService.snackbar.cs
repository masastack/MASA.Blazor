using Masa.Blazor.Popup.Components;
using Masa.Blazor.Presets;
using Masa.Blazor.Presets.EnqueuedSnackbars;

namespace Masa.Blazor;

public partial class PopupService
{
    public async Task EnqueueSnackbarAsync(string content)
    {
        if (!_enqueuedSnackbarsRendered)
        {
            await OpenAsync(typeof(EnqueuedSnackbars), new Dictionary<string, object>());
        }

        if (OnToastOpening is not null)
        {
            await OnToastOpening.Invoke(new ToastConfig()
            {
                Content = content
            });
        }
    }

    public Task ShowSnackbarAsync(string content)
    {
        return ShowSnackbarAsync(param => { param.Content = content; });
    }

    public Task ShowSnackbarAsync(string content, AlertTypes type)
    {
        return ShowSnackbarAsync(param =>
        {
            param.Content = content;
            param.Type = type;
        });
    }

    public Task ShowSnackbarAsync(Exception ex)
    {
        return ShowSnackbarAsync(param =>
        {
            param.Content = ex.Message;
            param.Type = AlertTypes.Error;
        });
    }

    public Task ShowSnackbarAsync(Action<SnackbarParameters> parameters)
    {
        SnackbarParameters param = new();

        parameters.Invoke(param);

        _ = OpenAsync(typeof(Snackbar), param.ToDictionary());

        return Task.CompletedTask;
    }
}
