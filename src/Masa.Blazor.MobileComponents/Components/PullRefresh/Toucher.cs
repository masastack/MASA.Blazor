namespace Masa.Blazor;

public class Toucher
{
    public double StartX { get; private set; }

    public double StartY { get; private set; }

    public double DeltaX { get; private set; }

    public double DeltaY { get; private set; }

    public double OffsetX { get; private set; }

    public double OffsetY { get; private set; }

    public string? Direction { get; private set; }

    public bool IsVertical => Direction == "vertical";

    public bool IsHorizontal => Direction == "horizontal";

    private void Reset()
    {
        StartX = 0;
        StartY = 0;
        OffsetX = 0;
        OffsetY = 0;
        Direction = null;
    }

    public void Start(TouchEventArgs e)
    {
        Reset();

        StartX = e.Touches[0].ClientX;
        StartY = e.Touches[0].ClientY;
    }

    public void Move(TouchEventArgs e)
    {
        var touch = e.Touches[0];

        DeltaX = (touch.ClientX < 0 ? 0 : touch.ClientX) - StartX;
        DeltaY = touch.ClientY - StartY;
        OffsetX = Math.Abs(DeltaX);
        OffsetY = Math.Abs(DeltaY);

        const int lockDirectionDistance = 10;

        if (Direction == null || (OffsetX < lockDirectionDistance && OffsetY < lockDirectionDistance))
        {
            Direction = GetDirection(OffsetX, OffsetY);
        }
    }

    private static string? GetDirection(double x, double y)
    {
        if (x > y)
        {
            return "horizontal";
        }

        if (y > x)
        {
            return "vertical";
        }

        return null;
    }
}
