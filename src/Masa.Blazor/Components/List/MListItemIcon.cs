namespace Masa.Blazor;

public class MListItemIcon : Container
{
    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "m-list-item__icon";
    }
}