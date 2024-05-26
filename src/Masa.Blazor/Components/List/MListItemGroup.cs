namespace Masa.Blazor;

public class MListItemGroup : MItemGroup
{
    public MListItemGroup() : base(GroupType.ListItemGroup)
    {
    }

    [Parameter] public string? Color { get; set; }

    private static Block _block = new("m-list-item-group");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(
            new[]
            {
                _block.Name,
                CssClassUtils.GetColor(Color, true)
            }
        );
    }
}