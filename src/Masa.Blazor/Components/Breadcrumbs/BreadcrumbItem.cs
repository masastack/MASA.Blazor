namespace Masa.Blazor;

public class BreadcrumbItem
{
    public bool Disabled { get; set; }

    public bool Exact { get; set; }

    public string? Href { get; set; }

    public string? Text { get; set; }
}