namespace Masa.Docs.Core.Models;

public class DefaultItem : IDefaultItem<DefaultItem>
{
    public string? Heading { get; set; }

    public bool Divider { get; set; }

    public string? Href { get; set; }

    public string? Icon { get; set; }

    public string? Title { get; set; }

    public NavItemState State { get; set; }

    public StringNumber Value { get; set; }

    public List<DefaultItem>? Children { get; set; }
}
