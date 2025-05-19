using Microsoft.JSInterop;

namespace Masa.Blazor.Components.Xgplayer;

public class XgplayerJSInteropHandle(MXgMusicPlayer player)
{
    [JSInvokable]
    public Task OnFullscreenChange(bool isFullscreen)
        => player.OnFullscreenChange.InvokeAsync(isFullscreen);

    [JSInvokable]
    public Task OnCssFullscreenChange(bool isCssFullscreen)
        => player.OnCssFullscreenChange.InvokeAsync(isCssFullscreen);

    [JSInvokable]
    public Task OnFullscreenTouchend()
        => player.OnFullscreenTouchend.InvokeAsync();
}
