namespace Masa.Blazor.Components.Drawflow;

public interface IDrawflowJSObjectReferenceProxy : IJSObjectReference
{
    Task SetMode(DrawflowEditorMode mode);

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

    Task RemoveNodeByIdAsync(int id);

    Task<string> ExportAsync();
}
