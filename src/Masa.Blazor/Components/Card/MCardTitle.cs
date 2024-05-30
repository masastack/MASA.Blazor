namespace Masa.Blazor;

public class MCardTitle : Container
{
    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "m-card__title";
    }
}