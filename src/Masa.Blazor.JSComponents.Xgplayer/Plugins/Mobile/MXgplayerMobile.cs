using Masa.Blazor.Components.Xgplayer.Plugins;
using Masa.Blazor.Components.Xgplayer.Plugins.Mobile;

namespace Masa.Blazor;

public class MXgplayerMobile : XgplayerPluginBase, IXgplayerMobile
{
    [Parameter]
    public bool DisableGesture { get; set; }

    [Parameter]
    [MasaApiParameter(true)]
    public bool GestureX { get; set; } = true;

    [Parameter]
    [MasaApiParameter(true)]
    public bool GestureY { get; set; } = true;

    [Parameter]
    [MasaApiParameter(0.25)]
    public double ScopeL { get; set; } = 0.25;

    [Parameter]
    [MasaApiParameter(0.25)]
    public double ScopeR { get; set; } = 0.25;

    [Parameter]
    [MasaApiParameter(2)]
    public double PressRate { get; set; } = 2.0;

    [Parameter]
    [MasaApiParameter(true)]
    public bool Darkness { get; set; } = true;

    [Parameter]
    [MasaApiParameter(0.6)]
    public double MaxDarkness { get; set; } = 0.6;

    [Parameter]
    [MasaApiParameter("normal")]
    public string Gradient { get; set; } = "normal";

    [Parameter]
    [MasaApiParameter(true)]
    public bool IsTouchingSeek { get; set; } = true;

    [Parameter]
    [MasaApiParameter(5)]
    public int MiniMoveStep { get; set; } = 5;

    [Parameter]
    public bool DisableActive { get; set; }

    [Parameter]
    public bool DisableTimeProgress { get; set; }

    [Parameter]
    [MasaApiParameter(true)]
    public bool HideControlsActive { get; set; } = true;

    [Parameter]
    public bool HideControlsEnd { get; set; }

    [Parameter]
    [MasaApiParameter(60 * 6 * 1000)]
    public int MoveDuration { get; set; } = 60 * 6 * 1000;

    [Parameter]
    [MasaApiParameter(true)]
    public bool DisablePress { get; set; } = true;

    [Parameter]
    public bool DisableSeekIcon { get; set; }
}