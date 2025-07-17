namespace Masa.Blazor.JSModules.LongPress;

public class LongPressJSModule(IJSRuntime js)
    : JSModule(js, $"./_content/Masa.Blazor/js/{JSManifest.LongPressIndexJs}"), ILongPressJSModule
{
    public async ValueTask<LongPressJSObject?> RegisterAsync(string selector, Func<Task> handle, int delay = 500)
    {
        try
        {
            var jsObject = await InvokeAsync<IJSObjectReference?>("register", selector,
                DotNetObjectReference.Create(new Invoker(handle)), delay);

            return jsObject is null ? null : new LongPressJSObject(jsObject);
        }
        catch (JSException e)
        {
            if (e.ForCannotCreateFromNullOrUndefined())
            {
                return null;
            }

            throw;
        }
    }
}