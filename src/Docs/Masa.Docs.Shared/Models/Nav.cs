namespace Masa.Docs.Shared;

public class Nav
{
    public string? Title { get; set; }

    public string? Subtitle { get; set; }

    public string? Tag { get; set; }

    public string? Href { get; set; }

    public string? Icon { get; set; }

    public List<Nav>? Children { get; set; }

    public bool HasChildren => Children is not null && Children.Any();
}
