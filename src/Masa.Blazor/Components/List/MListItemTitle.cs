namespace Masa.Blazor;

public class MListItemTitle : Container
{
    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "m-list-item__title";
    }
}