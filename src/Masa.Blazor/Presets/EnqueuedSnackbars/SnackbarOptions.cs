namespace Masa.Blazor.Presets;

public class SnackbarOptions
{
    public SnackbarOptions()
    {
        Id = Guid.NewGuid();
    }

    public SnackbarOptions(string content, AlertTypes type = AlertTypes.None, bool closeable = false, int timeout = 5000) : this()
    {
        Content = content;
        Type = type;
        Closeable = closeable;
        Timeout = timeout;
    }

    public SnackbarOptions(string title, string content, AlertTypes type = AlertTypes.None, bool closeable = false, int timeout = 5000)
        : this(content, type, closeable, timeout)
    {
        Title = title;
    }

    public SnackbarOptions(string content, string actionText, Func<Task> onAction, AlertTypes type = AlertTypes.None, bool closeable = true,
        int timeout = 5000)
        : this(content, type, closeable, timeout)
    {
        ActionText = actionText;
        OnAction = onAction;
    }

    public SnackbarOptions(string content, string actionText, Func<Task> onAction, string actionColor, AlertTypes type = AlertTypes.None,
        bool closeable = true, int timeout = 5000)
        : this(content, actionText, onAction, type, closeable, timeout)
    {
        ActionColor = actionColor;
    }

    public SnackbarOptions(string title, string content, string actionText, Func<Task> onAction, AlertTypes type = AlertTypes.None,
        bool closeable = true,
        int timeout = 5000)
        : this(title, content, type, closeable, timeout)
    {
        ActionText = actionText;
        OnAction = onAction;
    }

    public SnackbarOptions(string title, string content,  string actionText, Func<Task> onAction, string actionColor,
        AlertTypes type = AlertTypes.None,
        bool closeable = true, int timeout = 5000)
        : this(title, content, actionText, onAction, type, closeable, timeout)
    {
        ActionColor = actionColor;
    }

    public Guid Id { get; }

    public AlertTypes Type { get; set; }

    public bool Closeable { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public int Timeout { get; set; } = 5000;

    public string? ActionText { get; set; }

    public string? ActionColor { get; set; }

    public Func<Task>? OnAction { get; set; }
}
