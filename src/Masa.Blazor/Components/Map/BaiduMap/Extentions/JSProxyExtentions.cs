namespace Masa.Blazor
{
    public static class JSProxyExtentions
    {
        public static async ValueTask TryInvokeOriginVoidAsync(this IBaiduMapJSObjectReferenceProxy? obj, string identifier, params object[] args)
        {
            if (obj is null) 
                return;
            
            var origin = await obj.InvokeAsync<IJSObjectReference>("getOriginInstance");

            if (origin is null) 
                return;

            await origin.InvokeVoidAsync(identifier, args);
        }

        public static async ValueTask<TOut> TryInvokeOriginAsync<TOut>(this IBaiduMapJSObjectReferenceProxy? obj, string identifier, params object[] args)
        {
            if (obj is null)
                return default;

            var origin = await obj.InvokeAsync<IJSObjectReference>("getOriginInstance");

            if (origin is null) 
                return default;

            return await origin.InvokeAsync<TOut>(identifier, args);
        }
    }
}
