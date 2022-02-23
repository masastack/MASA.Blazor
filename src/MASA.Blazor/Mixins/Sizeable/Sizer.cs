namespace Masa.Blazor;

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

    bool Medium => !Small && !XSmall && !Large && !XLarge;

    public string SizeableClasses
    {
        get
        {
            var medium = Medium;

            var css = string.Empty;

            if (XSmall)
            {
                css += "m-size--x-small ";
            }

            if (Small)
            {
                css += "m-size--small ";
            }

            if (medium)
            {
                css += "m-size--default ";
            }

            if (Large)
            {
                css += "m-size--large ";
            }

            if (XLarge)
            {
                css += "m-size--x-large ";
            }

            return css[..^1];
        }
    }
}