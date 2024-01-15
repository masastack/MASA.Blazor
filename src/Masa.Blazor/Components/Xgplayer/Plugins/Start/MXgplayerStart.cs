using Masa.Blazor.Components.Xgplayer.Plugins;
using Masa.Blazor.Components.Xgplayer.Plugins.Start;

namespace Masa.Blazor;

public class MXgplayerStart : XgplayerPluginBase, IXgplayerStart
{
    // inherit
    [Parameter]
    public bool ShowAtPause { get; set; }

    // inherit
    [Parameter]
    public bool ShowAtEnd { get; set; }

    // inherit
    [Parameter]
    public bool DisableAnimate { get; set; }

    // inherit
    [Parameter]
    public StartMode Mode { get; set; }
}
