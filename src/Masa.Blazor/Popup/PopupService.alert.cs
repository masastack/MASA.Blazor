using Masa.Blazor.Popup.Components;

namespace Masa.Blazor;

public partial class PopupService
{
    public Task AlertAsync(string content)
    {
        return AlertAsync(param =>
        {
            param.Content = content;
        });
    }

    public Task AlertAsync(string content, AlertTypes type)
    {
        return AlertAsync(param =>
        {
            param.Content = content;
            param.Type = type;
        });
    }

    public Task AlertAsync(Exception ex)
    {
        return AlertAsync(param =>
        {
            param.Content = ex.Message;
            param.Type = AlertTypes.Error;
        });
    }

    public Task AlertAsync(Action<AlertParameters> parameters)
    {
        AlertParameters param = new();

        parameters.Invoke(param);

        _ = OpenAsync(typeof(Alert), param.ToDictionary());

        return Task.CompletedTask;
    }
}