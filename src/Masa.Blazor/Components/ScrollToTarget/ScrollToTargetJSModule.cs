namespace Masa.Blazor.Components.ScrollToTarget;

public class ScrollToTargetJSModule : JSModule
{
    public ScrollToTargetJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/components/scroll-to-target.js")
    {
    }

    public async ValueTask<ScrollToTargetJSObjectReference> InitAsync(
        DotNetObjectReference<ScrollToTargetJSInteropHandle> interopHandle,
        IntersectionObserverInit options
    )
    {
        var jsObjectReference = await InvokeAsync<IJSObjectReference>("init", interopHandle, options);
        return new ScrollToTargetJSObjectReference(jsObjectReference);
    }
}
