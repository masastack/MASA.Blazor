namespace Masa.Blazor.Components.Drawflow;

public class DrawflowJSObjectReferenceProxy : JSObjectReferenceProxy, IDrawflowJSObjectReferenceProxy
{
    public DrawflowJSObjectReferenceProxy(IJSObjectReference jsObjectReference) : base(jsObjectReference)
    {
    }

    public async Task SetMode(DrawflowEditorMode mode)
    {
        await InvokeVoidAsync("setMode", mode.ToString().ToLower());
    }

    public async Task<int> AddNodeAsync
    (
        string name,
        int inputs,
        int outputs,
        double clientX,
        double clientY,
        double offsetX,
        double offsetY,
        string? className,
        object? data,
        string html)
    {
        return await InvokeAsync<int>("addNode",
            name,
            inputs,
            outputs,
            clientX,
            clientY,
            offsetX,
            offsetY,
            className,
            data ?? new { },
            html);
    }

    public async Task RemoveNodeAsync(int nodeId)
    {
        await InvokeVoidAsync("removeNodeId", $"node-{nodeId}");
    }

    public async Task UpdateNodeDataAsync(int nodeId, object data)
    {
        await InvokeVoidAsync("updateNodeDataFromId", nodeId, data);
    }

    public async Task<string?> ExportAsync(bool withoutData = false)
    {
        return await InvokeAsync<string?>("export", withoutData);
    }

    public async Task AddInputAsync(int nodeId)
    {
        await InvokeVoidAsync("addNodeInput", nodeId);
    }

    public async Task AddOutputAsync(int nodeId)
    {
        await InvokeVoidAsync("addNodeOutput", nodeId);
    }

    public async Task RemoveInputAsync(int nodeId, string inputClass)
    {
        await InvokeVoidAsync("removeNodeInput", nodeId, inputClass);
    }

    public async Task RemoveOutputAsync(int nodeId, string outputClass)
    {
        await InvokeVoidAsync("removeNodeOutput", nodeId, outputClass);
    }
}

public enum DrawflowEditorMode
{
    Edit,

    Fixed,

    View
}
