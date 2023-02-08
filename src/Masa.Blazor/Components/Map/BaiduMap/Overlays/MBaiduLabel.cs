using System.Drawing;
using System.Text.Json.Serialization;

namespace Masa.Blazor
{
    public class MBaiduLabel : BComponentBase, IMapOverlay, ILabel
    {
        [Parameter]
        public string Content { get; set; }

        [Parameter]
        public GeoPoint Position { get; set; }

        [Parameter]
        public Size Offset { get; set; }

        [JsonIgnore]
        public IJSObjectReference OverlayRef { get; set; }

        [JsonIgnore]
        [CascadingParameter(Name = "Parent")]
        public MBaiduMap Parent { get; set; }

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
