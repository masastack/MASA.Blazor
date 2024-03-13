namespace Masa.Blazor;

public class MCardTitle : Container
{
    protected override IEnumerable<string> BuildComponentClass()
    {
        return new[] { "m-card__title" };
    }
}