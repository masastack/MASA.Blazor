using Masa.Blazor.JSModules;

namespace Masa.Blazor;

public class SwiperJSObjectReferenceProxy(IJSObjectReference jsObjectReference)
    : JSObjectReferenceBase(jsObjectReference)
{
    public async Task SlideToAsync(int index, int speed)
    {
        await JSObjectReference.InvokeVoidAsync("slideTo", index, speed);
    }

    public async Task SlideNextAsync(int speed)
    {
        await JSObjectReference.InvokeVoidAsync("slideNext", speed);
    }

    public async Task SlidePrevAsync(int speed)
    {
        await JSObjectReference.InvokeVoidAsync("slidePrev", speed);
    }

    public async Task UpdateAsync()
    {
        await JSObjectReference.InvokeVoidAsync("update");
    }

    protected override ValueTask DisposeAsyncCore()
    {
        return JSObjectReference.InvokeVoidAsync("dispose");
    }
}