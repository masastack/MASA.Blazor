
namespace Masa.Blazor
{
    public abstract class BaiduOverlayBase : MasaComponentBase
    {
        [JsonIgnore]
        internal IJSObjectReference? OverlayJSObjectRef { get; set; }

        [JsonIgnore]
        [CascadingParameter]
        protected MBaiduMap? MapRef { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && MapRef is not null)
                NextTick(async () => await MapRef.AddOverlayAsync(this));
        }

        public async Task ShowAsync() => await OverlayJSObjectRef.TryInvokeVoidAsync("show");

        public async Task HideAsync() => await OverlayJSObjectRef.TryInvokeVoidAsync("hide");
    }
}