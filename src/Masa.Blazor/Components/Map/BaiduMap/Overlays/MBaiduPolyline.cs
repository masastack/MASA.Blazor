using BlazorComponent.Attributes;
using System.Text.Json.Serialization;

namespace Masa.Blazor
{
    public class MBaiduPolyline : BComponentBase, IMapOverlay<MBaiduMap>, IPolyline, IStroke
    {
        [Parameter]
        public IEnumerable<GeoPoint> Points { get; set; }

        [Parameter]
        [ApiDefaultValue("#0000FF")]
        public string StrokeColor { get; set; } = "#0000FF";

        [Parameter]
        [ApiDefaultValue(0.9f)]
        public float StrokeOpacity { get; set; } = 0.9f;

        [Parameter]
        [ApiDefaultValue(2)]
        public float StrokeWeight { get; set; } = 2;

        [Parameter]
        [ApiDefaultValue(StrokeStyle.Solid)]
        public StrokeStyle StrokeStyle { get; set; } = StrokeStyle.Solid;

        [Parameter]
        public bool Geodesic { get; set; }

        [Parameter]
        [ApiDefaultValue(true)]
        public bool Clip { get; set; } = true;

        [JsonIgnore]
        public IJSObjectReference OverlayRef { get; set; }

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
