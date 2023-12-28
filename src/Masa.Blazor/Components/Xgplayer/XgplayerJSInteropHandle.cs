namespace Masa.Blazor.Components.Xgplayer;

public class XgplayerJSInteropHandle
{
    private readonly MXgMusicPlayer _player;

    public XgplayerJSInteropHandle(MXgMusicPlayer player)
    {
        _player = player;
    }

    [JSInvokable]
    public async Task OnFullscreenChange(bool isFullscreen)
    {
        await _player.OnFullscreenChange.InvokeAsync(isFullscreen);
    }

    [JSInvokable]
    public async Task OnCssFullscreenChange(bool isCssFullscreen)
    {
        await _player.OnCssFullscreenChange.InvokeAsync(isCssFullscreen);
    }
}
