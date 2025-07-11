namespace Masa.Blazor.Components.VideoSwiper;

public partial class MVideoControls : MasaComponentBase
{
    [Parameter] public string? Title { get; set; }

    [Parameter] public string? Subtitle { get; set; }

    [Parameter] public bool Fullscreen { get; set; }

    [Parameter] public bool IsPlaying { get; set; }

    [Parameter] public EventCallback OnPauseVideo { get; set; }

    [Parameter] public EventCallback OnPlayVideo { get; set; }

    [Parameter] public RenderFragment? ActionsContent { get; set; }

    [Parameter] public bool Available { get; set; }

    [Parameter] public EventCallback<bool> OnCloseFullscreen { get; set; }

    private static readonly Block _block = new("m-video-feed");
    private ModifierBuilder _controlsModifierBuilder = _block.Element("controls").CreateModifierBuilder();

    private async Task CloseFullscreen()
    {
        await OnCloseFullscreen.InvokeAsync(false);
    }
}