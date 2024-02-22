namespace Masa.Blazor;

public class MToolbarItems : Container
{
    protected override IEnumerable<string> BuildComponentClass()
    {
        return new[] { "m-toolbar__items" };
    }
}