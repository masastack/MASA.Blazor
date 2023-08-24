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

    public async Task RemoveNodeAsync(int id)
    {
        await InvokeVoidAsync("removeNodeId", $"node-{id}");
    }

    public async Task UpdateNodeDataAsync(int id, object data)
    {
        await InvokeVoidAsync("updateNodeDataFromId", id, data);
    }

    public async Task<string?> ExportAsync(bool withoutData = false)
    {
        return await InvokeAsync<string?>("export", withoutData);
    }
}

public enum DrawflowEditorMode
{
    Edit,

    Fixed,

    View
}
