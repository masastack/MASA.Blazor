using Masa.Blazor.Components.Xgplayer.Plugins;
using Masa.Blazor.Components.Xgplayer.Plugins.DynamicBg;

namespace Masa.Blazor;

public class MXgplayerDynamicBg : XgplayerPluginBase, IXgplayerDynamicBg
{
    [Parameter] public bool IsInnerRender { get; set; }
    [Parameter] public bool Disable { get; set; } = true;

    [Parameter] [MasaApiParameter(DynamicBgMode.Framerate)]
    public DynamicBgMode Mode { get; set; }

    [Parameter] public double FrameRate { get; set; } = 10;
    [Parameter] public string Filter { get; set; } = "blur(50px)";
    [Parameter] public bool AddMask { get; set; } = true;
    [Parameter] public string MaskBg { get; set; } = "rgba(0, 0, 0, 0.7)";
    [Parameter] public double Multiple { get; set; } = 1.2;
}