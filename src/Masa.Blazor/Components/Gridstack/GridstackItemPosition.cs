namespace Masa.Blazor;

public class GridstackItemPosition
{
    public int W { get; set; }

    public int H { get; set; }

    public int? X { get; set; }

    public int? Y { get; set; }

    public GridstackItemPosition()
    {
    }

    public GridstackItemPosition(int w, int h)
    {
        W = w;
        H = h;
    }

    public GridstackItemPosition(int w, int h, int x, int y) : this(w, h)
    {
        X = x;
        Y = y;
    }
}
