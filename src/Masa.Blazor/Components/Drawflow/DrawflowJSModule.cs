namespace Masa.Blazor.Components.Drawflow;

public class DrawflowJSModule : JSModule
{
    public DrawflowJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/proxies/drawflow-proxy.js")
    {
    }

    public async ValueTask<IDrawflowJSObjectReferenceProxy> Init(string selector, DrawflowEditorMode mode)
    {
        var jsObject = await InvokeAsync<IJSObjectReference>("init", selector, mode.ToString().ToLower());
        return new DrawflowJSObjectReferenceProxy(jsObject);
    }
}
