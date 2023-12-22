using System.Text.Json.Serialization;
using Masa.Blazor.Components.Xgplayer.Plugins;
using Masa.Blazor.Components.Xgplayer.Plugins.Play;

namespace Masa.Blazor;

public class MXgplayerPlay : XgplayerPluginBase, IXgplayerPlay
{
    // inherit
    [Parameter] public int Index { get; set; }

    // inherit
    [Parameter]
    [MasaApiParameter(PluginPosition.ControlsLeft)]
    public PluginPosition Position { get; set; } = PluginPosition.ControlsLeft;
}
