namespace Masa.Blazor
{
    public interface IBaiduMapJSObjectReferenceProxy : IJSObjectReference
    {
        ValueTask AddOverlayAsync(MBaiduOverlay overlay);
    }
}