namespace Masa.Blazor.Mixins.Menuable;

internal class Menuable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    internal Menuable(IJSRuntime jsRuntime)
    {
        _moduleTask = new Lazy<Task<IJSObjectReference>>(
            () => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import",
                "./_content/Masa.Blazor/js/mixins/menuable/index.js").AsTask());
    }

    internal async ValueTask<MenuableJSObjectResult> UseMenuableAsync(
        string activatorSelector,
        ElementReference contentRef,
        bool hasActivator,
        IMenuable2 options)
    {
        var moduleTask = await _moduleTask.Value;
        var jsObjectReference =
            await moduleTask.InvokeAsync<IJSObjectReference>("useMenuable", activatorSelector, contentRef,
                hasActivator,
                options);
        return CreateMenuableJSObjectResult(jsObjectReference);
    }

    private MenuableJSObjectResult CreateMenuableJSObjectResult(IJSObjectReference jsObjectReference)
    {
        return new MenuableJSObjectResult(UpdateDimensions);

        void UpdateDimensions()
        {
            _ = jsObjectReference.InvokeVoidAsync("updateDimensions");
        }
    }
}

internal record MenuableJSObjectResult(Action UpdateDimensions);