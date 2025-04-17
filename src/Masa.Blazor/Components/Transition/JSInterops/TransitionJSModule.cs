namespace Masa.Blazor.Components.Transition;

public class TransitionJSModule(IJSRuntime js)
    : JSModule(js, $"./_content/Masa.Blazor/js/{JSManifest.TransitionIndexJs}")
{
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