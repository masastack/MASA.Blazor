using System.Drawing;
using System.Text.Json.Serialization;

namespace Masa.Blazor
{
    public class MBaiduLabel : MBaiduOverlay, ILabel
    {
        [Parameter]
        public string Content { get; set; }

        [Parameter]
        public GeoPoint Position { get; set; }

        [Parameter]
        public Size Offset { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && MapRef is not null)
                NextTick(async () => OverlayJSObjectRef = await MapRef.AddOverlayAsync(this));
        }
    }
}
