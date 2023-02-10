namespace Masa.Blazor
{
    public abstract class MBaiduOverlay : BComponentBase
    {
        public virtual IJSObjectReference OverlayRef { get; set; }

        public virtual MBaiduMap MapRef { get; set; }

        public async Task ShowAsync() => await OverlayRef.InvokeVoidAsync("show");

        public async Task HideAsync() => await OverlayRef.InvokeVoidAsync("hide");
    }
}
