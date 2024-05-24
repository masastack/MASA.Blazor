using Masa.Blazor.Mixins.ScrollStrategy;

namespace Masa.Blazor.Mixins;

public class ScrollStrategyJSModule : JSModule
{
    private IJSObjectReference? _instance;

    public ScrollStrategyJSModule(IJSRuntime js) : base(js, "./_content/BlazorComponent/js/scrollStrategies.js")
    {
    }

    public bool Initialized { get; private set; }

    public async Task InitializeAsync(ElementReference root, ElementReference contentRef, ScrollStrategyOptions options)
    {
        _instance = await InvokeAsync<IJSObjectReference>("init", root, contentRef, options);

        Initialized = true;
    }

    public async Task BindAsync()
    {
        await _instance.TryInvokeVoidAsync("bind");
    }

    public async Task UnbindAsync()
    {
        await _instance.TryInvokeVoidAsync("unbind");
    }
}