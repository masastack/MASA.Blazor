namespace Masa.Blazor.Components.Xgplayer;

public class XgplayerJSInteropHandle
{
    private readonly MXgMusicPlayer _player;

    public XgplayerJSInteropHandle(MXgMusicPlayer player)
    {
        _player = player;
    }

    [JSInvokable]
    public Task OnFullscreenChange(bool isFullscreen)
        => _player.OnFullscreenChange.InvokeAsync(isFullscreen);

    [JSInvokable]
    public Task OnCssFullscreenChange(bool isCssFullscreen)
        => _player.OnCssFullscreenChange.InvokeAsync(isCssFullscreen);

    [JSInvokable]
    public Task OnFullscreenTouchend()
        => _player.OnFullscreenTouchend.InvokeAsync();
}
