namespace Masa.Blazor.JSModules;

public interface IResizeJSModule
{
    ValueTask ObserverAsync(ElementReference el, Func<Task> handle);

    ValueTask UnobserveAsync(ElementReference el);

    ValueTask ObserverAsync(string selector, Func<Task> handle);

    ValueTask UnobserveAsync(string selector);
}