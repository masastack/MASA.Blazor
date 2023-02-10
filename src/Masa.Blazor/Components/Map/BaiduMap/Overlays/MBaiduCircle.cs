using BlazorComponent.Attributes;
using System.Text.Json.Serialization;

namespace Masa.Blazor
{
    public class MBaiduCircle : MBaiduOverlay, ICircle, IStroke, IFillable
    {
        [Parameter]
        public GeoPoint Center { get; set; }

        [Parameter]
        public float Radius { get; set; }

        [Parameter]
        [ApiDefaultValue("blue")]
        public string StrokeColor { get; set; } = "blue";

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

        [JsonIgnore]
        [CascadingParameter(Name = "Parent")]
        public override MBaiduMap MapRef { get; set; }

        [JsonIgnore]
        public override IJSObjectReference OverlayRef { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && MapRef is not null)
                NextTick(async () => await MapRef.AddOverlayAsync(this));
        }

    }
}
