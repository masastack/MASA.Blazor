namespace Masa.Blazor;

public class MTabItem : MWindowItem, ITabItem
{
    [CascadingParameter] public MTabs? Tabs { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Tabs?.RegisterTabItem(this);
    }

    protected override ValueTask DisposeAsyncCore()
    {
        Tabs?.UnregisterTabItem(this);

        return base.DisposeAsyncCore();
    }
}