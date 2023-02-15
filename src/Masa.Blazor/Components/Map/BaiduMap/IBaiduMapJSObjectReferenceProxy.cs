namespace Masa.Blazor
{
    public interface IBaiduMapJSObjectReferenceProxy: IJSObjectReference
    {
        ValueTask AddOverlayAsync(MBaiduOverlay overlay);

        ValueTask RemoveOverlayAsync(MBaiduOverlay overlay);

        ValueTask ClearOverlaysAsync();
    }
}
