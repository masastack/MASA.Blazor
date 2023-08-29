namespace Masa.Blazor.Components.Drawflow;

public class DrawflowInteropHandle
{
    private readonly MDrawflow _drawflow;

    public DrawflowInteropHandle(MDrawflow drawflow)
    {
        _drawflow = drawflow;
    }

    [JSInvokable]
    public async Task OnNodeCreated(string nodeId)
    {
        await _drawflow.OnNodeCreated.InvokeAsync(nodeId);
    }

    [JSInvokable]
    public async Task OnNodeRemoved(string nodeId)
    {
        await _drawflow.OnNodeRemoved.InvokeAsync(nodeId);
    }

    [JSInvokable]
    public async Task OnNodeDataChanged(string nodeId)
    {
        await _drawflow.OnNodeDataChanged.InvokeAsync(nodeId);
    }

    [JSInvokable]
    public async Task OnImport()
    {
        await _drawflow.OnImport.InvokeAsync();
    }
}
