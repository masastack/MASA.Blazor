using Masa.Blazor.Components.Xgplayer.Plugins;
using Masa.Blazor.Components.Xgplayer.Plugins.Controls;

namespace Masa.Blazor;

public class MXgplayerControls : XgplayerPluginBase, IXgplayerControls
{
    // inherit
    [Parameter] [MasaApiParameter(true)] public bool AutoHide { get; set; } = true;

    // inherit
    [Parameter] [MasaApiParameter(nameof(ControlsMode.Normal))]
    public ControlsMode Mode { get; set; }

    // inherit
    [Parameter] public bool InitShow { get; set; }
}
