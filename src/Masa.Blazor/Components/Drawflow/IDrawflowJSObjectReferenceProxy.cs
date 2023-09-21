namespace Masa.Blazor.Components.Drawflow;

public interface IDrawflowJSObjectReferenceProxy : IJSObjectReference, IDrawflow
{
    Task SetMode(DrawflowEditorMode mode);
}

public interface IDrawflow
{
    Task<string> AddNodeAsync
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

    Task RemoveNodeAsync(string nodeId);

    Task UpdateNodeDataAsync(string nodeId, object data);

    Task UpdateNodeHTMLAsync(string nodeId, string html);

    Task ClearAsync();

    Task<string?> ExportAsync(bool indented = false);

    Task ImportAsync(string json);

    Task AddInputAsync(string nodeId);

    Task AddOutputAsync(string nodeId);

    Task RemoveInputAsync(string nodeId, string inputClass);

    Task RemoveOutputAsync(string nodeId, string inputClass);

    Task FocusNodeAsync(string nodeId);

    Task CenterNodeAsync(string nodeId, bool animate);
}
