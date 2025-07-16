using Masa.Blazor.Components.Xgplayer;
using Masa.Blazor.JSComponents.Xgplayer;
using Masa.Blazor.JSModules;
using Microsoft.JSInterop;

namespace Masa.Blazor;

public class XgplayerJSObjectReference(IJSObjectReference jsObjectReference) : JSObjectReferenceBase(jsObjectReference)
{
    public async ValueTask UpdateUrlAsync(XgplayerUrl url)
    {
        await InvokeInstanceVoidAsync("playNext", new { url });
    }

    public async ValueTask<XgplayerPropsAndStates> GetPropsAndStatesAsync()
    {
        return await JSObjectReference.InvokeAsync<XgplayerPropsAndStates>("getPropsAndStates");
    }

    public async ValueTask ToMusicPlayerAsync()
    {
        await JSObjectReference.InvokeVoidAsync("toMusic");
    }

    public async ValueTask ToVideoPlayerAsync()
    {
        await JSObjectReference.InvokeVoidAsync("toVideo");
    }

    public async Task TogglePlayAsync(bool? force = null)
    {
        await JSObjectReference.InvokeVoidAsync("togglePlay", force);
    }

    public async Task ToggleMutedAsync(bool? force = null)
    {
        await JSObjectReference.InvokeVoidAsync("toggleMuted", force);
    }

    public async Task SetPropAsync(string prop, object value)
    {
        await JSObjectReference.InvokeVoidAsync("setProp", prop, value);
    }


    public async Task DestroyAsync()
    {
        await JSObjectReference.InvokeVoidAsync("destroy");
    }
}