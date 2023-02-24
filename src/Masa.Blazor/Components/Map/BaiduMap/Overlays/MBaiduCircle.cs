using BlazorComponent.Attributes;

namespace Masa.Blazor
{
    public class MBaiduCircle : BaiduOverlayBase, ICircle, IStroke, IFillable
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

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            RenderConditions = () => Radius > 0;
        }
    }
}