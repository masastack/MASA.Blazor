using Masa.Blazor.Components.Xgplayer.Plugins;
using Masa.Blazor.Components.Xgplayer.Plugins.Start;

namespace Masa.Blazor;

public class MXgplayerVolume : XgplayerPluginBase, IXgplayerVolume
{
    [Parameter] public PluginPosition Position { get; set; }
    [Parameter] public int Index { get; set; }
    [Parameter] public bool ShowValueLabel { get; set; }
    [Parameter] [MasaApiParameter(0.6)] public float Default { get; set; } = 0.6f;
    [Parameter] [MasaApiParameter(0.2)] public float MiniVolume { get; set; } = 0.2f;
}