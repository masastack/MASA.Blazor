namespace Masa.Blazor.Components.ScrollToTarget;

public class ScrollToTargetJSModule(IJSRuntime js)
    : JSModule(js, $"./_content/Masa.Blazor/js/{JSManifest.ScrollToTargetIndexJs}")
{
    public async ValueTask<ScrollToTargetJSObjectReference> InitAsync(
        DotNetObjectReference<ScrollToTargetJSInteropHandle> interopHandle,
        IntersectionObserverInit options
    )
    {
        var jsObjectReference = await InvokeAsync<IJSObjectReference>("init", interopHandle, options);
        return new ScrollToTargetJSObjectReference(jsObjectReference);
    }
}
