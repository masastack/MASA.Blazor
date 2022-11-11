using Masa.Docs.Shared.Shared;

namespace Masa.Docs.Shared;

public class NavItem : IDefaultItem<NavItem>
{
    public NavItem()
    {
    }

    public NavItem(string title)
    {
        Title = title;
    }

    public string Title { get; set; } = null!;

    public StringNumber Value { get; set; }

    public List<NavItem>? Children { get; set; }

    public string? Group { get; set; }

    public string? Heading { get; set; }

    public bool Divider { get; set; }

    public string? Href { get; set; }

    public string? Icon { get; set; }

    public string Segment => (Group ?? Title);
}
