namespace Masa.Blazor;

public class MCardSubtitle : Container
{
    protected override IEnumerable<string> BuildComponentClass()
    {
        return new[] { "m-card__subtitle" };
    }
}