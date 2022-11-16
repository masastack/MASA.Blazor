namespace Masa.Blazor;

public class GridstackWidgetPosition
{
    public int W { get; set; }

    public int H { get; set; }

    public int? X { get; set; }

    public int? Y { get; set; }

    public GridstackWidgetPosition()
    {
    }

    public GridstackWidgetPosition(int w, int h)
    {
        W = w;
        H = h;
    }

    public GridstackWidgetPosition(int w, int h, int x, int y) : this(w, h)
    {
        X = x;
        Y = y;
    }
}
