namespace Masa.Blazor;

public class MCardActions : Container
{
    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "m-card__actions";
    }
}