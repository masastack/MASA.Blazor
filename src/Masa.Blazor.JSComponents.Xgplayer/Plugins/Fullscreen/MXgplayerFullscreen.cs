using Masa.Blazor.Components.Xgplayer.Plugins;
using Masa.Blazor.Components.Xgplayer.Plugins.CssFullscreen;

namespace Masa.Blazor;

public class MXgplayerFullscreen : XgplayerPluginBase, IXgplayerFullscreen
{
    [Parameter] [MasaApiParameter(PluginPosition.ControlsRight)]
    public PluginPosition Position { get; set; } = PluginPosition.ControlsRight;

    [Parameter] public int Index { get; set; } = 0;
    [Parameter] public string? Target { get; set; }
    [Parameter] public bool RotateFullscreen { get; set; }
    [Parameter] public bool UseCssFullscreen { get; set; }
    [Parameter] public bool NeedBackIcon { get; set; }
    [Parameter] public bool UseScreenOrientation { get; set; }
    [Parameter] public string? LockOrientationType { get; set; } = "landscape";
}