﻿namespace Masa.Blazor.JSModules;

public class JSObjectReferenceBase : IAsyncDisposable
{
    protected IJSObjectReference JSObjectReference { get; init; }

    public JSObjectReferenceBase(IJSObjectReference jsObjectReference)
    {
        JSObjectReference = jsObjectReference;
    }

    public async ValueTask InvokeVoidAsync(string identity, params object[] args)
    {
        await JSObjectReference.InvokeVoidAsync("invokeVoid", args.Prepend(identity).ToArray());
    }

    public async ValueTask DisposeAsync()
    {
        await JSObjectReference.DisposeAsync();
    }
}