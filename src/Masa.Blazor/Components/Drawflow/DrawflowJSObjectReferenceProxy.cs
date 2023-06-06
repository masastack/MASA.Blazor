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
        int positionX,
        int positionY,
        string? className,
        object? data,
        string html)
    {
        return await InvokeAsync<int>("addNode",
            name,
            inputs,
            outputs,
            positionX,
            positionY,
            className,
            data ?? new { },
            html);
    }

    public async Task RemoveNodeByIdAsync(int id)
    {
        await InvokeVoidAsync("removeNodeId", $"node-{id}");
    }

    public async Task<string> ExportAsync()
    {
        return await InvokeAsync<string>("export");
    }
}

public enum DrawflowEditorMode
{
    Edit,

    Fixed,

    View
}
