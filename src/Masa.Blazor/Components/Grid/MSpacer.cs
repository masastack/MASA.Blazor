namespace Masa.Blazor;

public class MSpacer : Container
{
    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "spacer";
    }
}