using Masa.Blazor.Mixins.ScrollStrategy;

namespace Masa.Blazor.Mixins;

public class ScrollStrategyJSModule(IJSRuntime js)
    : JSModule(js, "./_content/Masa.Blazor/js/scrollStrategies.js?version=2")
{
    public async ValueTask<ScrollStrategyResult> CreateScrollStrategy(ElementReference root,
        ElementReference contentRef, ScrollStrategyOptions options)
    {
        var instance = await InvokeAsync<IJSObjectReference>("useScrollStrategies", options, root, contentRef, null);

        return new ScrollStrategyResult(
            () => _ = instance!.InvokeVoidAsync("bind"),
            () => _ = instance!.InvokeVoidAsync("unbind"),
            () => _ = instance!.DisposeAsync()
        );
    }

    public async ValueTask<ScrollStrategyResult> CreateScrollStrategy<TComponent>(ElementReference root,
        ElementReference contentRef, ScrollStrategyOptions options,
        DotNetObjectReference<TComponent> dotNetObjectReference) where TComponent : class
    {
        var instance = await InvokeAsync<IJSObjectReference>("useScrollStrategies", options, root, contentRef, null,
            dotNetObjectReference);

        return new ScrollStrategyResult(
            () => _ = instance!.InvokeVoidAsync("bind"),
            () => _ = instance!.InvokeVoidAsync("unbind"),
            () => _ = instance!.DisposeAsync()
        );
    }
}