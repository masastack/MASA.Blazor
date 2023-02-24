using BlazorComponent.Attributes;

namespace Masa.Blazor
{
    public class MBaiduPolyline : BaiduOverlayBase, IPolyline, IStroke
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

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            RenderConditions = () => Points is not null;
        }

    }
}