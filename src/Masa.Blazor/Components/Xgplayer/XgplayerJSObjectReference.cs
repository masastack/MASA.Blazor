using Masa.Blazor.Components.Xgplayer;

namespace Masa.Blazor;

public class XgplayerJSObjectReference : IAsyncDisposable
{
    private readonly IJSObjectReference _jsObjectReference;

    public XgplayerJSObjectReference(IJSObjectReference jsObjectReference)
    {
        _jsObjectReference = jsObjectReference;
    }

    public async ValueTask UpdateUrlAsync(XgplayerUrl url)
    {
        await InvokeVoidAsync("playNext", new { url });
    }

    public async ValueTask InvokeVoidAsync(string identity, params object[] args)
    {
        await _jsObjectReference.InvokeVoidAsync("invokeVoid", identity, args);
    }

    public async ValueTask<XgplayerPropsAndStates> GetPropsAndStatesAsync()
    {
        return await _jsObjectReference.InvokeAsync<XgplayerPropsAndStates>("getPropsAndStates");
    }

    public async ValueTask SwitchToMusicAsync(XgplayerUrl url)
    {
        await _jsObjectReference.InvokeVoidAsync("switchMusic", url);
    }

    public async ValueTask SwitchToVideoAsync(XgplayerUrl url)
    {
        await _jsObjectReference.InvokeVoidAsync("switchVideo", url);
    }

    public async Task DestroyAsync()
    {
        await _jsObjectReference.InvokeVoidAsync("destroy");
    }

    public async ValueTask DisposeAsync()
    {
        await _jsObjectReference.DisposeAsync();
    }
}
