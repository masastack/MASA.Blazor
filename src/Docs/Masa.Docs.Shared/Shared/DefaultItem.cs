namespace Masa.Docs.Shared.Shared;

public class DefaultItem : IDefaultItem<DefaultItem>
{
    public string? Heading { get; set; }

    public bool Divider { get; set; }

    public string? Href { get; set; }

    public string? Icon { get; set; }

    public string? Title { get; set; }

    public StringNumber Value { get; set; }
    public List<DefaultItem>? Children { get; set; }
    public bool HasChildren => Children.Any();
}

public interface IDefaultItem<TItem>
{
    string? Heading { get; }

    bool Divider { get; set; }

    string? Href { get; set; }

    string? Icon { get; set; }

    string? Title { get; set; }

    StringNumber Value { get; set; }

    List<TItem>? Children { get; }

    bool HasChildren()
    {
        return Children is not null && Children.Any();
    }
}
