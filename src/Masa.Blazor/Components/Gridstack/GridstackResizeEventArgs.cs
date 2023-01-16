namespace Masa.Blazor;

public class GridstackResizeEventArgs : EventArgs
{
    public GridstackResizeEventArgs(string id, int x, int y, int w, int h, double width, double height)
    {
        Id = id;
        X = x;
        Y = y;
        W = w;
        H = h;
        Width = width;
        Height = height;
    }

    public string Id { get; }

    public int X { get; set; }

    public int Y { get; set; }

    public int W { get; set; }

    public int H { get; set; }

    public double Width { get; }

    public double Height { get; }
}
