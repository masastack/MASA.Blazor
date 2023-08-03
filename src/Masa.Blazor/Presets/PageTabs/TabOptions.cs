namespace Masa.Blazor.Presets;

public class TabOptions
{
    public string? Title { get; set; }

    public string? Icon { get; set; }

    public string? Class { get; set; }

    public string? TitleClass { get; set; }

    public string? TitleStyle { get; set; }

    public TabOptions()
    {
    }

    public TabOptions(string? title)
    {
        Title = title;
    }

    public TabOptions(string? title, string? icon) : this(title)
    {
        Icon = icon;
    }

    public TabOptions(string? title, string? icon, string? titleClass) : this(title, icon)
    {
        TitleClass = titleClass;
    }

    public TabOptions(string? title, string? icon, string? titleClass, string? @class) : this(title, icon, titleClass)
    {
        Class = @class;
    }
}
