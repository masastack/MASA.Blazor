namespace Masa.Blazor.JSModules.LongPress;

public record LongPressJSObject(IJSObjectReference jsObject)
{
    public async Task DisposeAsync()
    {
        await jsObject.InvokeVoidAsync("dispose");
        await jsObject.DisposeAsync();
    }
}