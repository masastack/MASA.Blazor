namespace Masa.Blazor;

public class MTabsItems : MWindow
{
    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(
            new[]
            {
                "m-tabs-items"
            });
    }
}