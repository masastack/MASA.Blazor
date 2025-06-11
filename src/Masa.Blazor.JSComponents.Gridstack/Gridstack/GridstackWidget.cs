namespace Masa.Blazor;

public class GridstackWidget : GridstackWidgetPosition
{
    public GridstackWidget()
    {
    }

    public GridstackWidget(int w, int h) : base(w, h)
    {
    }

    public GridstackWidget(int w, int h, int x, int y) : base(w, h, x, y)
    {
    }

    public string Id { get; set; }
    public bool Locked { get; set; }
    public bool NoResize { get; set; }
    public bool NoMove { get; set; }

    /// <summary>
    /// Make gridItem size itself to the content.
    /// false by default,  
    /// </summary>
    public BooleanNumber? SizeToContent { get; set; }

    internal string LockedAttr => Locked ? "yes" : "no";
    internal string NoResizeAttr => NoResize ? "yes" : "no";
    internal string NoMoveAttr => NoMove ? "yes" : "no";

    internal string? SizeToContentAttr => SizeToContent is null ? null :
        SizeToContent.IsBoolean() ? (SizeToContent.AsT0 ? "yes" : "false") : SizeToContent.AsT1.ToString();
}