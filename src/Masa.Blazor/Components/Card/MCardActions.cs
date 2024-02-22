namespace Masa.Blazor;

public class MCardActions : Container
{
    protected override IEnumerable<string> BuildComponentClass()
    {
        return new[] { "m-card__actions" };
    }
}