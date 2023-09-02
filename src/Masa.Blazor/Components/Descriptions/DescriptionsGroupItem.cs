namespace Masa.Blazor;

public class DescriptionsGroupItem
{
    public int Span { get; init; }

    public string? Label { get; init; }

    public RenderFragment? ChildContent { get; init; }

    public string? LabelStyle { get; init; }

    public string? LabelClass { get; init; }

    public string? Class { get; init; }

    public string? Style { get; init; }
}
