using System.Drawing;
using System.Text.Json.Serialization;

namespace Masa.Blazor
{
    public class MBaiduMarker : BComponentBase, IMapOverlay, IMarker
    {
        [JsonIgnore]
        [CascadingParameter(Name = "Parent")]
        public MBaiduMap Parent { get; set; }

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
