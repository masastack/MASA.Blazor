namespace Microsoft.JSInterop;

public static class IJSRuntimeExtensions
{
    public static async ValueTask TryInvokeVoidAsync(this IJSRuntime jsRuntime, string identifier, params object?[]? args)
    {
        try
        {
            await jsRuntime.InvokeVoidAsync(identifier, args);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}
