namespace Masa.Blazor;

public class SwiperJSObjectReferenceProxy : ISwiperJSObjectReferenceProxy
{
    private readonly IJSObjectReference _jsObjectReference;

    public SwiperJSObjectReferenceProxy(IJSObjectReference jsObjectReference)
    {
        _jsObjectReference = jsObjectReference;
    }

    public async Task SlideToAsync(int index, int speed)
    {
        await _jsObjectReference.InvokeVoidAsync("slideTo", index, speed);
    }

    public async Task SlideNextAsync(int speed)
    {
        await _jsObjectReference.InvokeVoidAsync("slideNext", speed);
    }

    public async Task SlidePrevAsync(int speed)
    {
        await _jsObjectReference.InvokeVoidAsync("slidePrev", speed);
    }

    public async Task DisposeAsync()
    {
        await _jsObjectReference.InvokeVoidAsync("dispose");
    }
}
