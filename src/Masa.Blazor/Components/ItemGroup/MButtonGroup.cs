namespace Masa.Blazor;

public class MButtonGroup : MItemGroup
{
    public MButtonGroup() : base(GroupType.ButtonGroup)
    {
    }

    [Parameter] public bool Borderless { get; set; }

    [Parameter] public bool Dense { get; set; }

    [Parameter] public bool Group { get; set; }

    [Parameter] public bool Rounded { get; set; }

    [Parameter] public bool Shaped { get; set; }

    [Parameter] public bool Tile { get; set; }

    [Parameter] public string? BackgroundColor { get; set; }

    [Parameter] public string? Color { get; set; }

    private Block _block = new("m-btn-toggle");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(
            _block.Modifier(Borderless)
                .And(Dense)
                .And(Group)
                .And(Rounded)
                .And(Shaped)
                .And(Tile)
                .AddTextColor(Color)
                .AddBackgroundColor(BackgroundColor, !Group)
                .GenerateCssClasses()
        );
    }
}