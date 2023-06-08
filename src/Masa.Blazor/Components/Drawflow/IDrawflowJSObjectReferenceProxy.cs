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
        int positionX,
        int positionY,
        string? className,
        object? data,
        string html);

    Task RemoveNodeAsync(int id);

    Task UpdateNodeDataAsync(int id, object data);

    Task<string?> ExportAsync(bool withoutData = false);
}
