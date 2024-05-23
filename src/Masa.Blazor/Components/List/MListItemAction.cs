namespace Masa.Blazor;

public class MListItemAction : Container
{
    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "m-list-item__action";
    }
}