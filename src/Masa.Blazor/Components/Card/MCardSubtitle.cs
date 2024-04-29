namespace Masa.Blazor;

public class MCardSubtitle : Container
{
    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "m-card__subtitle";
    }
}