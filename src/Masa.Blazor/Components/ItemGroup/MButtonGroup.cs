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

    private static Block _block = new("m-btn-toggle");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(
            new[]
            {
                _modifierBuilder.Add(
                        Borderless,
                        Dense,
                        Group,
                        Rounded,
                        Shaped,
                        Tile)
                    .AddTextColor(Color)
                    .AddBackgroundColor(BackgroundColor, !Group)
                    .Build()
            }
        );
    }
}