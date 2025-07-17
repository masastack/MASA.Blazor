namespace Masa.Blazor;

public class MListItemAction : Container
{
    [Parameter] public bool Stack { get; set; }

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "m-list-item__action";
        if (Stack)
        {
            yield return "m-list-item__action--stack";
        }
    }
}