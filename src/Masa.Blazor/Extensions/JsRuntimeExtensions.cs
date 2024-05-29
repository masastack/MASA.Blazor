using System.Text.Json;

namespace Masa.Blazor.Extensions;

public static class JsRuntimeExtensions
{
    public static async Task AddHtmlElementEventListener(this IJSRuntime jsRuntime, string selector, string type,
        Action callback,
        OneOf<EventListenerOptions, bool> options, EventListenerExtras? extras = null)
    {
        await jsRuntime.InvokeVoidAsync(JsInteropConstants.AddHtmlElementEventListener,
            selector,
            type,
            DotNetObjectReference.Create(new Invoker(callback)),
            options.Value,
            extras
        );
    }

    public static async Task AddHtmlElementEventListener(this IJSRuntime jsRuntime, string selector, string type,
        Func<Task> callback,
        OneOf<EventListenerOptions, bool> options, EventListenerExtras? extras = null)
    {
        await jsRuntime.InvokeVoidAsync(JsInteropConstants.AddHtmlElementEventListener,
            selector,
            type,
            DotNetObjectReference.Create(new Invoker(callback)),
            options.Value,
            extras
        );
    }

    public static async Task AddHtmlElementEventListener(this IJSRuntime jsRuntime, ElementReference el, string type,
        Func<Task> callback,
        OneOf<EventListenerOptions, bool> options, EventListenerExtras? extras = null)
    {
        await jsRuntime.InvokeVoidAsync(JsInteropConstants.AddHtmlElementEventListener,
            el.GetSelector(),
            type,
            DotNetObjectReference.Create(new Invoker(callback)),
            options.Value,
            extras
        );
    }


    public static async Task AddHtmlElementEventListener<T>(this IJSRuntime jsRuntime, string selector, string type,
        Func<T, Task> callback,
        OneOf<EventListenerOptions, bool> options, EventListenerExtras? extras = null)
    {
        await jsRuntime.InvokeVoidAsync(JsInteropConstants.AddHtmlElementEventListener,
            selector,
            type,
            DotNetObjectReference.Create(new Invoker<T>(callback)),
            options.Value,
            extras
        );
    }

    public static async Task AddHtmlElementEventListener<T>(this IJSRuntime jsRuntime, ElementReference el, string type,
        Func<T, Task> callback,
        OneOf<EventListenerOptions, bool> options, EventListenerExtras? extras = null)
    {
        await jsRuntime.InvokeVoidAsync(JsInteropConstants.AddHtmlElementEventListener,
            el.GetSelector(),
            type,
            DotNetObjectReference.Create(new Invoker<T>(callback)),
            options.Value,
            extras
        );
    }

    public static async Task RemoveHtmlElementEventListener(this IJSRuntime jsRuntime, string selector, string type,
        string? key = null)
    {
        await jsRuntime.InvokeVoidAsync(JsInteropConstants.RemoveHtmlElementEventListener, selector, type, key);
    }

    public static async Task ScrollTo(this IJSRuntime jsRuntime, string selector, double? top, double? left = null,
        ScrollBehavior behavior = ScrollBehavior.Smooth)
    {
        await jsRuntime.InvokeVoidAsync(JsInteropConstants.ScrollTo, selector,
            new ScrollToOptions(left, top, behavior));
    }

    public static async Task ScrollTo(this IJSRuntime jsRuntime, ElementReference el, double? top, double? left = null,
        ScrollBehavior behavior = ScrollBehavior.Smooth)
    {
        if (el.TryGetSelector(out var selector))
        {
            await jsRuntime.ScrollTo(selector, top, left, behavior);
        }
    }

    public static async Task ScrollIntoView(this IJSRuntime jsRuntime, ElementReference el,
        ScrollLogicalPosition? block,
        ScrollLogicalPosition? inline, ScrollBehavior behavior = ScrollBehavior.Smooth)
    {
        await jsRuntime.InvokeVoidAsync(JsInteropConstants.ScrollIntoView, el,
            new ScrollIntoViewOptions(block, inline, behavior));
    }

    public static async Task ScrollToElement(this IJSRuntime jsRuntime, string selector, int offset,
        ScrollBehavior behavior = ScrollBehavior.Smooth)
    {
        await jsRuntime.InvokeVoidAsync(JsInteropConstants.ScrollToElement, selector, offset,
            behavior.ToString().ToLower());
    }

    public static async Task ScrollIntoParentView(this IJSRuntime jsRuntime, string selector, bool inline = false,
        bool start = false,
        int level = 1, ScrollBehavior behavior = ScrollBehavior.Smooth)
    {
        await jsRuntime.InvokeVoidAsync(JsInteropConstants.ScrollIntoParentView, selector, inline, start, level,
            behavior.ToString().ToLower());
    }

    public static async Task ScrollIntoParentView(this IJSRuntime jsRuntime, ElementReference el, bool inline = false,
        bool start = false,
        int level = 1, ScrollBehavior behavior = ScrollBehavior.Smooth)
    {
        if (el.TryGetSelector(out var selector))
        {
            await jsRuntime.ScrollIntoParentView(selector, inline, start, level, behavior);
        }
    }

    public static Task<double?> GetScrollWidthAsync(this IJSRuntime jsRuntime, ElementReference el)
    {
        return jsRuntime.GetNumberPropAsync(el, "scrollWidth");
    }

    public static Task<double?> GetOffsetWidthAsync(this IJSRuntime jsRuntime, ElementReference el)
    {
        return jsRuntime.GetNumberPropAsync(el, "offsetWidth");
    }

    public static Task<double?> GetWindowPageYOffsetAsync(this IJSRuntime jsRuntime)
    {
        return jsRuntime.GetNumberPropAsync("window", "pageYOffset");
    }

    public static ValueTask<BoundingClientRect> GetBoundingClientRectAsync(this IJSRuntime jsRuntime,
        ElementReference el, string? attach = ".m-application")
    {
        return jsRuntime.InvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, el,
            attach);
    }

    public static ValueTask<BoundingClientRect> GetBoundingClientRectAsync(this IJSRuntime jsRuntime,
        string selector, string? attach = ".m-application")
    {
        return jsRuntime.InvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, selector,
            attach);
    }

    public static ValueTask DispatchEventAsync(this IJSRuntime jsRuntime, ElementReference el, string type,
        bool stopPropagation)
    {
        return jsRuntime.InvokeVoidAsync(JsInteropConstants.TriggerEvent, el, type, stopPropagation);
    }

    public static ValueTask SetPropertyAsync(this IJSRuntime jsRuntime, ElementReference el, string key, object value)
    {
        return jsRuntime.InvokeVoidAsync(JsInteropConstants.SetProperty, el, key, value);
    }
    
    // TODO: how about return double instead of double? ?
    private static async Task<double?> GetNumberPropAsync(this IJSRuntime jsRuntime, object el, string name)
    {
        var jsonElement = await jsRuntime.InvokeAsync<JsonElement>(JsInteropConstants.GetProp, el, name);
        return jsonElement.ValueKind == JsonValueKind.Number ? jsonElement.GetDouble() : null;
    }
}