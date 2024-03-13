namespace Masa.Blazor;

public class MToolbarTitle : Container
{
    protected override IEnumerable<string> BuildComponentClass()
    {
        return new[] { "m-toolbar__title" };
    }
}