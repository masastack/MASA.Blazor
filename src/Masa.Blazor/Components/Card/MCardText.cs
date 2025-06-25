namespace Masa.Blazor;

public class MCardText : Container
{
    protected override IEnumerable<string?> BuildComponentClass()
    {
        yield return "m-card__text";
    }
}