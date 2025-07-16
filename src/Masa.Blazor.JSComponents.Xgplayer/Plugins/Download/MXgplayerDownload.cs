using Masa.Blazor.Components.Xgplayer.Plugins;
using Masa.Blazor.Components.Xgplayer.Plugins.CssFullscreen;

namespace Masa.Blazor;

public class MXgplayerCssFullscreen : XgplayerPluginBase, IXgplayerDownload
{
    [Parameter] [MasaApiParameter(PluginPosition.ControlsRight)]
    public PluginPosition Position { get; set; } = PluginPosition.ControlsRight;

    [Parameter] public int Index { get; set; } = 1;
    [Parameter] public bool Disable { get; set; }
    [Parameter] public string? Target { get; set; }
}