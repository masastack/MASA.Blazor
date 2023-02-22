using BlazorComponent.Attributes;

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

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            RenderConditions = () => Points is not null;
        }
    }
}