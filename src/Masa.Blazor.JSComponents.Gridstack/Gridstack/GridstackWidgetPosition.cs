namespace Masa.Blazor;

public class GridstackWidgetPosition
{
    private int _w;
    private int _h;

    public int W
    {
        get => GetValidValue(_w);
        set => _w = value;
    }

    public int H
    {
        get => GetValidValue(_h);
        set => _h = value;
    }

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

    private int GetValidValue(int val) => val == 0 ? 1 : val;
}
