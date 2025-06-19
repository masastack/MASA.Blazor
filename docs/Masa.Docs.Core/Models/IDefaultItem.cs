using System.Diagnostics.CodeAnalysis;

namespace Masa.Docs.Core.Models;

public interface IDefaultItem<TItem>
{
    string? Heading { get; }

    bool Divider { get; set; }

    string? Href { get; set; }

    bool Exact { get; set; }

    string? Icon { get; set; }

    string? Title { get; set; }

    string? State { get; set; }
    
    string? StateBackgroundColor { get; set; }
    
    string? ReleasedOn { get; set; }

    List<TItem>? Children { get; }
    
    ComponentType ComponentType { get; }

    [MemberNotNullWhen(true, nameof(Children))]
    bool HasChildren()
    {
        return Children is not null && Children.Count > 0;
    }

    string? Target
    {
        get
        {
            if (Href == null)
            {
                return null;
            }

            return Href.StartsWith("http") ? "_blank" : "_self";
        }
    }
}
