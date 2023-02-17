using BlazorComponent.Attributes;
using System.Text.Json.Serialization;

namespace Masa.Blazor
{
    public class MBaiduPolygon : MBaiduOverlay, IPolygon, IStroke, IFillable
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
        [ApiDefaultValue("blue")]
        public string FillColor { get; set; } = "blue";

        [Parameter]
        [ApiDefaultValue(0.3f)]
        public float FillOpacity { get; set; } = 0.3f;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && MapRef is not null)
                NextTick(async () => OverlayJSObjectRef = await MapRef.AddOverlayAsync(this));
        }
    }
}
