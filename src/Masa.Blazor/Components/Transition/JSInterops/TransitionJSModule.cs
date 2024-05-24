namespace Masa.Blazor.Components.Transition;

public class TransitionJSModule : JSModule
{
    public TransitionJSModule(IJSRuntime js) : base(js, "./_content/BlazorComponent/js/transition.js?version=v1.4.0")
    {
    }

    public bool Initialized { get; private set; }

    public async ValueTask<IJSObjectReference?> InitAsync(ElementReference elementReference,
        DotNetObjectReference<TransitionJsInteropHandle> interopHandle)
    {
        try
        {
            var jsObject = await InvokeAsync<IJSObjectReference>("init", elementReference, interopHandle);
            Initialized = true;
            return jsObject;
        }
        catch (JSException e) when (e.ForCannotCreateFromNullOrUndefined())
        {
            return null;
        }
    }
}