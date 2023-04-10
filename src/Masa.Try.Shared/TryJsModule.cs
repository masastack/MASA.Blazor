using BlazorComponent.JSInterop;
using Microsoft.JSInterop;

namespace Masa.Try.Shared;

public class TryJSModule : JSModule
{
    public TryJSModule(IJSRuntime js) : base(js, "./_content/Masa.Try.Shared/js/try.js")
    {
    }

    public async ValueTask Init()
    {
        await InvokeVoidAsync("init");
    }

    public async ValueTask AddScript(ScriptNode scriptNode) => await InvokeVoidAsync("addScript", scriptNode);
}