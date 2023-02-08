using System.Drawing;
using System.Text.Json.Serialization;

namespace Masa.Blazor
{
    public class MBaiduMarker : BComponentBase, IMapOverlay<MBaiduMap>, IMarker
    {
        [JsonIgnore]
        public IJSObjectReference OverlayRef { get; set; }

        [Parameter]
        public GeoPoint Point { get; set; }

        [Parameter]
        public Size Offset { get; set; }

        [Parameter]
        public float Rotation { get; set; }

        [Parameter]
        public string Title { get; set; }

        [JsonIgnore]
        [CascadingParameter(Name = "Parent")]
        public MBaiduMap MapRef { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && MapRef is not null)
            {
                OverlayRef = await MapRef.Module.ConstructOverlayAsync<IMapOverlay<MBaiduMap>, MBaiduMap>(this);
                NextTick(async () => await MapRef.AddOverlayAsync<IMapOverlay<MBaiduMap>, MBaiduMap>(this));
            }
        }
    }
}
