using System;
using System.Text.Json;

namespace Masa.Blazor.Extensions;

public static class JsRuntimeExtensions
{
    /// <summary>
    /// Add an event listener to an HTML element, returning a unique ID for the listener.
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="selector"></param>
    /// <param name="type"></param>
    /// <param name="callback"></param>
    /// <param name="options"></param>
    /// <param name="extras"></param>
    /// <returns></returns>
    public static async Task<long> AddHtmlElementEventListener(this IJSRuntime jsRuntime, string selector, string type,
        Action callback,
        OneOf<EventListenerOptions, bool> options, EventListenerExtras? extras = null)
    {
        return await jsRuntime.InvokeAsync<long>(JsInteropConstants.AddHtmlElementEventListener,
            selector,
            type,
            DotNetObjectReference.Create(new Invoker(callback)),
            options.Value,
            extras
        );
    }

    /// <summary>
    /// Add an event listener to an HTML element, returning a unique ID for the listener.
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="selector"></param>
    /// <param name="type"></param>
    /// <param name="callback"></param>
    /// <param name="options"></param>
    /// <param name="extras"></param>
    /// <returns></returns>
    public static async Task<long> AddHtmlElementEventListener(this IJSRuntime jsRuntime, string selector, string type,
        Func<Task> callback,
        OneOf<EventListenerOptions, bool> options, EventListenerExtras? extras = null)
    {
        return await jsRuntime.InvokeAsync<long>(JsInteropConstants.AddHtmlElementEventListener,
            selector,
            type,
            DotNetObjectReference.Create(new Invoker(callback)),
            options.Value,
            extras
        );
    }

    /// <summary>
    /// Add an event listener to an HTML element, returning a unique ID for the listener.
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="el"></param>
    /// <param name="type"></param>
    /// <param name="callback"></param>
    /// <param name="options"></param>
    /// <param name="extras"></param>
    /// <returns></returns>
    public static async Task<long> AddHtmlElementEventListener(this IJSRuntime jsRuntime, ElementReference el, string type,
        Func<Task> callback,
        OneOf<EventListenerOptions, bool> options, EventListenerExtras? extras = null)
    {
        if (el.TryGetSelector(out var selector))
        {
            return await jsRuntime.InvokeAsync<long>(JsInteropConstants.AddHtmlElementEventListener,
                selector,
                type,
                DotNetObjectReference.Create(new Invoker(callback)),
                options.Value,
                extras
            );
        }

        return -1;
    }

    /// <summary>
    /// Add an event listener to an HTML element, returning a unique ID for the listener.
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="selector"></param>
    /// <param name="type"></param>
    /// <param name="callback"></param>
    /// <param name="options"></param>
    /// <param name="extras"></param>
    /// <typeparam name="TEventArgs"></typeparam>
    /// <returns></returns>
    public static async Task<long> AddHtmlElementEventListener<TEventArgs>(this IJSRuntime jsRuntime, string selector, string type,
        Func<TEventArgs, Task> callback,
        OneOf<EventListenerOptions, bool> options, EventListenerExtras? extras = null)
    {
        return await jsRuntime.InvokeAsync<long>(JsInteropConstants.AddHtmlElementEventListener,
            selector,
            type,
            DotNetObjectReference.Create(new Invoker<TEventArgs>(callback)),
            options.Value,
            extras
        );
    }

    /// <summary>
    /// Add an event listener to an HTML element, returning a unique ID for the listener.
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="el"></param>
    /// <param name="type"></param>
    /// <param name="callback"></param>
    /// <param name="options"></param>
    /// <param name="extras"></param>
    /// <typeparam name="TEventArgs"></typeparam>
    /// <returns></returns>
    public static async Task<long> AddHtmlElementEventListener<TEventArgs>(this IJSRuntime jsRuntime, ElementReference el,
        string type,
        Func<TEventArgs, Task> callback,
        OneOf<EventListenerOptions, bool> options, EventListenerExtras? extras = null)
    {
        if (el.TryGetSelector(out var selector))
        {
            return await jsRuntime.InvokeAsync<long>(JsInteropConstants.AddHtmlElementEventListener,
                selector,
                type,
                DotNetObjectReference.Create(new Invoker<TEventArgs>(callback)),
                options.Value,
                extras
            );
        }

        return -1;
    }

    /// <summary>
    /// Remove an event listener from an HTML element using the unique ID returned by AddHtmlElementEventListener.
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="eventId"></param>
    public static async Task RemoveHtmlElementEventListener(this IJSRuntime jsRuntime, long eventId)
    {
        if (eventId <= 0) return;
        await jsRuntime.InvokeVoidAsync(JsInteropConstants.RemoveHtmlElementEventListener, eventId);
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

    public static async Task ScrollTo(this IJSRuntime jsRuntime, IJSObjectReference el, double? top, double? left = null,
        ScrollBehavior behavior = ScrollBehavior.Smooth)
    {
        await jsRuntime.InvokeVoidAsync(JsInteropConstants.ScrollTo, el, new ScrollToOptions(left, top, behavior));
    }

    /// <summary>
    /// Scroll to a specific position in the document.
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="top"></param>
    /// <param name="left"></param>
    /// <param name="behavior"></param>
    public static async Task ScrollTo(this IJSRuntime jsRuntime, double? top, double? left = null,
        ScrollBehavior behavior = ScrollBehavior.Smooth)
    {
        await jsRuntime.InvokeVoidAsync(JsInteropConstants.ScrollTo, "document", new ScrollToOptions(left, top, behavior));
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

    /// <summary>
    /// Dispatch an event on the element
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="el"></param>
    /// <param name="eventName">The name of the event, e.g. MouseEvent, KeyboardEvent, etc.</param>
    /// <param name="eventType">The type of the event, e.g. click, keydown, etc.</param>
    /// <param name="stopPropagation">Whether to stop propagation</param>
    /// <returns></returns>
    public static ValueTask DispatchEventAsync(this IJSRuntime jsRuntime, ElementReference el, string eventName, string eventType,
        bool stopPropagation)
    {
        return jsRuntime.InvokeVoidAsync(JsInteropConstants.TriggerEvent, el, eventName, eventType, stopPropagation);
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

    /// <summary>
    /// Toggle mourning mode
    /// </summary>
    /// <param name="jsRuntime">The instance of IJSRuntime</param>
    /// <param name="force">Force the mourning mode on or off</param>
    public static async Task ToggleMourningModeAsync(this IJSRuntime jsRuntime, bool force)
    {
        await jsRuntime.InvokeVoidAsync(JsInteropConstants.ToggleMourningMode, force);
    }
}