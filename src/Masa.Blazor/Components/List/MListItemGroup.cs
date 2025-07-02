namespace Masa.Blazor;

public class MListItemGroup : MItemGroup
{
    public MListItemGroup()
    {
        GroupType = GroupType.ListItemGroup;
    }

    [Parameter] public string? Color { get; set; }

    private static Block _block = new("m-list-item-group");
    
    private readonly Dictionary<string, IDictionary<string, object?>> _defaults 
        = new() { [nameof(MListItem)] = new Dictionary<string, object?>() };

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat([_block.Name]);
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        
        _defaults[nameof(MListItem)][nameof(MListItem.ActiveColor)] = Color;
    }

    protected override void BuildRenderTree(RenderTreeBuilder __builder)
    {
        __builder.OpenComponent<MDefaultsProvider>(0);
        __builder.AddAttribute(0, nameof(MDefaultsProvider.Defaults), _defaults);
        __builder.AddAttribute(1, nameof(MDefaultsProvider.ChildContent), (RenderFragment)base.BuildRenderTree);
        __builder.CloseComponent();
    }
}