namespace Masa.Docs.Core.Models;

public interface IDefaultItem<TItem>
{
    string? Heading { get; }

    bool Divider { get; set; }

    string? Href { get; set; }

    string? Icon { get; set; }

    string? Title { get; set; }

    string? Tag { get; set; }

    StringNumber Value { get; set; }

    List<TItem>? Children { get; }

    bool HasChildren()
    {
        return Children is not null && Children.Any();
    }
}