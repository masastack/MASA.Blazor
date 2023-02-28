using Masa.Blazor.Presets;

namespace Masa.Blazor;

public partial class PopupService
{
    public event Func<SnackbarOptions, Task> SnackbarOpen;

    public async Task EnqueueSnackbarAsync(string content, AlertTypes type = AlertTypes.None, bool closeable = false, int timeout = 5000)
    {
        await EnqueueSnackbarAsync(new SnackbarOptions(content, type, closeable, timeout));
    }

    public async Task EnqueueSnackbarAsync(string title, string content, AlertTypes type = AlertTypes.None, bool closeable = false,
        int timeout = 5000)
    {
        await EnqueueSnackbarAsync(new SnackbarOptions(title, content, type, closeable, timeout));
    }

    public async Task EnqueueSnackbarAsync(Exception exception, bool withStackTrace, bool closeable = false, int timeout = 5000)
    {
        if (withStackTrace)
        {
            await EnqueueSnackbarAsync(exception.Message, exception.StackTrace, AlertTypes.Error, closeable, timeout);
        }
        else
        {
            await EnqueueSnackbarAsync(exception.Message, AlertTypes.Error, closeable, timeout);
        }
    }

    public async Task EnqueueSnackbarAsync(SnackbarOptions options)
    {
        if (SnackbarOpen is null) return;

        await SnackbarOpen.Invoke(options);
    }
}
