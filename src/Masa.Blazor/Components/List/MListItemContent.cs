namespace Masa.Blazor;

public class MListItemContent : Container
{
    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "m-list-item__content";
    }
}