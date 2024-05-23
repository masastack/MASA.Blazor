namespace Masa.Blazor;

public class MListItemGroup : MItemGroup
{
    public MListItemGroup() : base(GroupType.ListItemGroup)
    {
    }

    [Parameter] public string? Color { get; set; }

    private Block _block = new("m-list-item-group");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(
            _block.AddTextColor(Color).GenerateCssClasses()
        );
    }
}