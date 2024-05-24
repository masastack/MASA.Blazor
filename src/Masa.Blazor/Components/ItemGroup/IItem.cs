namespace Masa.Blazor.Components.ItemGroup;

public interface IItem : IGroupable
{
    RenderFragment? ChildContent { get; set; }
}