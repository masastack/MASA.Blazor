using Masa.Blazor.Components.ItemGroup;

namespace Masa.Blazor.Components.Tabs;

public interface ITabItem : IItem
{
    string? Transition { get; set; }

    string? ReverseTransition { get; set; }
}