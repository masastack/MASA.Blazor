using Masa.Blazor.Components.Drawflow;

namespace Masa.Blazor.Playground.Services;

public class DrawflowService : IDrawflow
{
    private MDrawflow? _drawflow;

    public void SetContainer(MDrawflow drawflow)
    {
        _drawflow = drawflow;
    }

    public async Task<int> AddNodeAsync(string name, int inputs, int outputs, int positionX, int positionY, string? className, object? data,
        string html)
    {
        if (_drawflow == null)
        {
            return 0;
        }

        return await _drawflow.AddNodeAsync(name, inputs, outputs, positionX, positionY, className, data, html);
    }

    public async Task RemoveNodeAsync(int id)
    {
        if (_drawflow == null) return;

        await _drawflow.RemoveNodeAsync(id);
    }

    public async Task UpdateNodeDataAsync(int id, object data)
    {
        if (_drawflow == null) return;

        await _drawflow.UpdateNodeDataAsync(id, data);
    }

    public async Task<string?> ExportAsync(bool withoutData = false)
    {
        if (_drawflow == null) return null;

        return await _drawflow.ExportAsync(withoutData);
    }
}
