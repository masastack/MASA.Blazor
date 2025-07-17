using Masa.Blazor.JSComponents.Xgplayer;
using Microsoft.JSInterop;

namespace Masa.Blazor.Components.Xgplayer;

public class XgplayerJSInteropHandle(MXgMusicPlayer player)
{
    [JSInvokable]
    public Task OnFullscreenChange(bool isFullscreen)
        => player.OnFullscreenChange.InvokeAsync(isFullscreen);

    [JSInvokable]
    public Task OnCssFullscreenChange(bool isCssFullscreen) =>
        player.OnCssFullscreenChange.InvokeAsync(isCssFullscreen);

    [JSInvokable]
    public Task OnFullscreenTouchend() => player.OnFullscreenTouchend.InvokeAsync();

    [JSInvokable]
    public Task OnMetadataLoaded(VideoMetadata metadata)
        => player.OnVideoMetadataLoaded.InvokeAsync(metadata);

    [JSInvokable]
    public Task OnResize(VideoSize size) => player.OnVideoResize.InvokeAsync(size);

    [JSInvokable]
    public Task OnPlay() => player.OnPlay.InvokeAsync();

    [JSInvokable]
    public Task OnPause() => player.OnPause.InvokeAsync();

    [JSInvokable]
    public Task OnError() => player.OnError.InvokeAsync();

    [JSInvokable]
    public Task OnEnded() => player.OnEnded.InvokeAsync();

    [JSInvokable]
    public Task OnReady() => player.OnReady.InvokeAsync();
}