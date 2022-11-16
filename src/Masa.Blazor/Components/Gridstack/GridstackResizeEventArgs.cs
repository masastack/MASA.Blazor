namespace Masa.Blazor;

public class GridstackResizeEventArgs : EventArgs
{
    public GridstackResizeEventArgs(string id, int width, int height)
    {
        Id = id;
        Width = width;
        Height = height;
    }

    public string Id { get; }

    public int Width { get; }

    public int Height { get; }
}
