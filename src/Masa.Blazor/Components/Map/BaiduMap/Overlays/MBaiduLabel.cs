using System.Drawing;

namespace Masa.Blazor
{
    public class MBaiduLabel : BaiduOverlayBase, ILabel
    {
        [Parameter]
        public string Content { get; set; }

        [Parameter]
        public GeoPoint Position { get; set; }

        [Parameter]
        public Size Offset { get; set; }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            RenderConditions = () => Content is not null;
        }
    }
}