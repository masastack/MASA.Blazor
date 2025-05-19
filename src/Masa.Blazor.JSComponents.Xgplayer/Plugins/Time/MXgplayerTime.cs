using Masa.Blazor.Components.Xgplayer.Plugins;
using Masa.Blazor.Components.Xgplayer.Plugins.Time;

namespace Masa.Blazor;

public class MXgplayerTime : XgplayerPluginBase, IXgplayerTime
{
    // inherit
    [Parameter]
    [MasaApiParameter(2)]
    public int Index { get; set; } = 2;

    // inherit
    [Parameter]
    [MasaApiParameter(PluginPosition.ControlsLeft)]
    public PluginPosition Position { get; set; } = PluginPosition.ControlsLeft;
}
