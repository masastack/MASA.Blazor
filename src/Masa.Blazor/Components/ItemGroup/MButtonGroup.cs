namespace Masa.Blazor;

public class MButtonGroup : MItemGroup
{
    public MButtonGroup()
    {
        GroupType = GroupType.ButtonGroup;
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

    private readonly Dictionary<string, IDictionary<string, object?>> _defaults 
        = new() { [nameof(MButton)] = new Dictionary<string, object?>() };

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        _defaults[nameof(MButton)][nameof(MButton.ActiveColor)] = Color;
        _defaults[nameof(MButton)][nameof(MButton.Color)] = BackgroundColor;
    }

    protected override void BuildRenderTree(RenderTreeBuilder __builder)
    {
        __builder.OpenComponent<MDefaultsProvider>(0);
        __builder.AddAttribute(1, nameof(MDefaultsProvider.Defaults), _defaults);
        __builder.AddAttribute(2, nameof(MDefaultsProvider.ChildContent), (RenderFragment)base.BuildRenderTree);
        __builder.CloseComponent();
    }

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
                    .Build()
            }
        );
    }
}