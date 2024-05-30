namespace Masa.Blazor;

public class MListItemSubtitle : Container
{
    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "m-list-item__subtitle";
    }
}