using Masa.Blazor.Components.Xgplayer;
using Masa.Blazor.JSModules;

namespace Masa.Blazor;

public class XgplayerJSObjectReference : JSObjectReferenceBase
{
    public XgplayerJSObjectReference(IJSObjectReference jsObjectReference) : base(jsObjectReference)
    {
    }

    public async ValueTask UpdateUrlAsync(XgplayerUrl url)
    {
        await InvokeVoidAsync("playNext", new { url });
    }

    public async ValueTask<XgplayerPropsAndStates> GetPropsAndStatesAsync()
    {
        return await JSObjectReference.InvokeAsync<XgplayerPropsAndStates>("getPropsAndStates");
    }

    public async ValueTask SwitchToMusicAsync(XgplayerUrl url)
    {
        await JSObjectReference.InvokeVoidAsync("switchMusic", url);
    }

    public async ValueTask SwitchToVideoAsync(XgplayerUrl url)
    {
        await JSObjectReference.InvokeVoidAsync("switchVideo", url);
    }

    public async Task DestroyAsync()
    {
        await JSObjectReference.InvokeVoidAsync("destroy");
    }
}
