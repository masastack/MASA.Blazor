namespace MASA.Blazor;

public class Sizer : ISizeable
{
    public Sizer()
    {
    }

    public Sizer(ISizeable sizeable)
    {
        Large = sizeable.Large;
        Small = sizeable.Small;
        XLarge = sizeable.XLarge;
        XSmall = sizeable.XSmall;
    }
    
    public bool Large { get; set; }
    public bool Small { get; set; }
    public bool XLarge { get; set; }
    public bool XSmall { get; set; }
}