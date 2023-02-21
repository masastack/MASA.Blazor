namespace Masa.Blazor
{
    public interface IBaiduMapJSObjectReferenceProxy: IJSObjectReference
    {
        ValueTask<IJSObjectReference> AddOverlayAsync(MBaiduOverlay overlay);
    }
}
