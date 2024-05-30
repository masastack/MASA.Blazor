namespace Masa.Blazor;

public class MListItemActionText : Container
{
    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "m-list-item__action-text";
    }
}