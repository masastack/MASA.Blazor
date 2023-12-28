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
        Console.Out.WriteLine("[XgplayerJSInteropHandle] OnFullscreenChange");
        await _player.OnFullscreenChange.InvokeAsync(isFullscreen);
    }

    [JSInvokable]
    public async Task OnCssFullscreenChange(bool isCssFullscreen)
    {
        Console.Out.WriteLine("[XgplayerJSInteropHandle] OnCssFullscreenChange");
        await _player.OnCssFullscreenChange.InvokeAsync(isCssFullscreen);
    }
}
