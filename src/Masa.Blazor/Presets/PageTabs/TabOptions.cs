namespace Masa.Blazor.Presets;

public class TabOptions(bool closeable = true)
{
    public string? Title { get; set; }

    public string? Icon { get; set; }

    public string? Class { get; set; }

    public string? TitleClass { get; set; }

    public string? TitleStyle { get; set; }

    public bool Closeable { get; set; } = closeable;

    internal bool Fixed { get; set; }

    internal bool ShowCloseBtn => Closeable && !Fixed;

    public TabOptions(string? title, bool closeable = true) : this(closeable)
    {
        Title = title;
    }

    public TabOptions(string? title, string? icon, bool closeable = true) : this(title, closeable)
    {
        Icon = icon;
    }

    public TabOptions(string? title, string? icon, string? titleClass, bool closeable = true) : this(title, icon, closeable)
    {
        TitleClass = titleClass;
    }

    public TabOptions(string? title, string? icon, string? titleClass, string? @class, bool closeable = true) : this(title, icon, titleClass, closeable)
    {
        Class = @class;
    }
}
