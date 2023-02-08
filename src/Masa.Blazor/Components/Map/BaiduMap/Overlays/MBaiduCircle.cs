using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Masa.Blazor
{
    public class MBaiduCircle : BComponentBase, IMapOverlay, ICircle, IStroke, IFillable
    {
        [Parameter]
        public GeoPoint Center { get; set; }

        [Parameter]
        public float Radius { get; set; }

        [Parameter]
        [DefaultValue("#00D3FC")]
        public string StrokeColor { get; set; } = "#00D3FC";

        [Parameter]
        [DefaultValue(0.9f)]
        public float StrokeOpacity { get; set; } = 0.9f;

        [Parameter]
        [DefaultValue(2)]
        public float StrokeWeight { get; set; } = 2;

        [Parameter]
        [DefaultValue(StrokeStyle.Solid)]
        public StrokeStyle StrokeStyle { get; set; } = StrokeStyle.Solid;

        [Parameter]
        [DefaultValue("#00B2D5")]
        public string FillColor { get; set; } = "#00B2D5";

        [Parameter]
        [DefaultValue(0.5f)]
        public float FillOpacity { get; set; } = 0.5f;

        [JsonIgnore]
        [CascadingParameter(Name = "Parent")]
        public MBaiduMap Parent { get; set; }

        [JsonIgnore]
        public IJSObjectReference OverlayRef { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && Parent is not null)
            {
                OverlayRef = await Parent.Module.ConstructOverlayAsync(this);
                NextTick(async () => await Parent.AddOverlayAsync(this));
            }
        }

    }
}
