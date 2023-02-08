using System.Text.Json.Serialization;
using Util.Reflection.Expressions;

namespace Masa.Blazor
{
    public class MBaiduCircle : BComponentBase, IMapOverlay, ICircle, IStroke, IFillable
    {
        [Parameter]
        public GeoPoint Center { get; set; }

        [Parameter]
        public float Radius { get; set; }

        [Parameter]
        public string StrokeColor { get; set; } = "#00D3FC";

        [Parameter]
        public float StrokeOpacity { get; set; } = 0.9f;

        [Parameter]
        public float StrokeWeight { get; set; } = 2;

        [Parameter]
        public StrokeStyle StrokeStyle { get; set; } = StrokeStyle.Solid;

        [Parameter]
        public string FillColor { get; set; } = "#00B2D5";

        [Parameter]
        public float FillOpacity { get; set; } = 0.5f;

        [JsonIgnore]
        [CascadingParameter(Name = "Parent")]
        public MBaiduMap Parent { get; set; }

        [JsonIgnore]
        public IJSObjectReference OverlayRef { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                if (Parent is not null)
                    OverlayRef = await Parent.Module.ConstructOverlayAsync(this);

                NextTick(async () => await Parent.AddOverlayAsync(this));
            }
        }

    }
}
