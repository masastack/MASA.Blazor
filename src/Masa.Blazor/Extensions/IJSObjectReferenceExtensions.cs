namespace BlazorComponent
{
    public static class IJSObjectReferenceExtensions
    {
        public static async ValueTask TryInvokeVoidAsync(this IJSObjectReference? jsObjectReference, string identifier, params object?[]? args)
        {
            if (jsObjectReference is null)
                return;

            await jsObjectReference.InvokeVoidAsync(identifier, args);
        }

        public static async ValueTask<TValue?> TryInvokeAsync<TValue>(this IJSObjectReference? jsObjectReference, string identifier,
            params object?[]? args)
        {
            if (jsObjectReference is null)
                return default;

            return await jsObjectReference.InvokeAsync<TValue>(identifier, args);
        }

        public static async ValueTask TryDisposeAsync(this IJSObjectReference? jsObjectReference)
        {
            if (jsObjectReference is null) return;

            await jsObjectReference.DisposeAsync();
        }
    }
}
