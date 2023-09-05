namespace Masa.Blazor.Components.Drawflow;

public interface IDrawflowJSObjectReferenceProxy : IJSObjectReference, IDrawflow
{
    Task SetMode(DrawflowEditorMode mode);
}

public interface IDrawflow
{
    Task<int> AddNodeAsync
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
        string html);

    Task<DrawflowNode<TData>?> GetNodeFromIdAsync<TData>(string nodeId);

    Task RemoveNodeAsync(int nodeId);

    Task UpdateNodeDataAsync(int nodeId, object data);

    Task UpdateNodeHTMLAsync(int nodeId, string html);

    Task ClearAsync();

    Task<string?> ExportAsync(bool indented = false);

    Task ImportAsync(string json);

    Task AddInputAsync(int nodeId);

    Task AddOutputAsync(int nodeId);

    Task RemoveInputAsync(int nodeId, string inputClass);

    Task RemoveOutputAsync(int nodeId, string inputClass);

    Task FocusNodeAsync(string nodeId);

    Task CenterNodeAsync(string nodeId, bool animate);
}
