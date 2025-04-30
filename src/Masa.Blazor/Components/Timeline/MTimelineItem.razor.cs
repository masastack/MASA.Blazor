namespace Masa.Blazor;

public partial class MTimelineItem : IThemeable
{
    [CascadingParameter(Name = "Reverse")] public bool Reverse { get; set; }

    [Parameter]
    [MasaApiParameter("primary")]
    public string? Color { get; set; } = "primary";

    [Parameter] public bool FillDot { get; set; }

    [Parameter] public bool HideDot { get; set; }

    [Parameter] public string? Icon { get; set; }

    [Parameter] public string? IconColor { get; set; }

    [Parameter] public bool Large { get; set; }

    [Parameter] public bool Left { get; set; }

    [Parameter] public bool Right { get; set; }

    [Parameter] public bool Small { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public RenderFragment? OppositeContent { get; set; }

    [Parameter] public RenderFragment? IconContent { get; set; }

    private static Block _block = new("m-timeline-item");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private ModifierBuilder _dotModifierBuilder = _block.Element("dot").CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _modifierBuilder
            .Add(FillDot)
            .Add("before", Reverse ? Right : Left)
            .Add("after", Reverse ? Left : Right)
            .AddTheme(ComputedTheme)
            .Build();
    }
}