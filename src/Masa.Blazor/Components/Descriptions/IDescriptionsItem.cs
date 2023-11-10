namespace Masa.Blazor;

public interface IDescriptionsItem
{
    string? Label { get; }

    int Span { get; }

    RenderFragment? ChildContent { get; }

    string? LabelStyle { get; }

    string? LabelClass { get; }

    string? Class { get; }

    string? Style { get; }
}
