using System.Text.Json.Serialization;

namespace Masa.Blazor
{
    public abstract class MBaiduOverlay : BComponentBase
    {
        [JsonIgnore]
        internal IJSObjectReference OverlayJSObjectRef { get; set; }

        [JsonIgnore]
        [CascadingParameter]
        protected MBaiduMap MapRef { get; set; }

        public async Task ShowAsync() => await OverlayJSObjectRef.TryInvokeVoidAsync("show");

        public async Task HideAsync() => await OverlayJSObjectRef.TryInvokeVoidAsync("hide");
    }
}